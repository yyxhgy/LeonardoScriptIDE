using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeonardoScriptCore.Serial
{
    public class SerialPortClient : ISerialPortClient, IDisposable
    {
        public static string[] GetAvailableComs()
        {
            return SerialPort.GetPortNames(); ;
        }
        private static IList<SerialPortClient> _OpenComs
        {
            get;
            set;
        } = new List<SerialPortClient>();

        private SerialPort _SerialPort;
        private Thread _WriteThread;
        private Thread _ReadThread;
        private Thread _MainThread;
        private readonly ConcurrentQueue<DataStream> _WriteQueue = new ConcurrentQueue<DataStream>();
        private ManualResetEvent _WriteLock = new ManualResetEvent(false);
        public event EventHandler OnSessionClosed;
        private bool _Running = false;
        private ReceiveFilter _Filter = new ReceiveFilter();
        private MessageEncoder _Encoder = new MessageEncoder();
        private ConcurrentQueue<Tuple<ICommand, PackageInfo>> _MessageQueue
        {
            get;
        } = new ConcurrentQueue<Tuple<ICommand, PackageInfo>>();

        public int BaudRate
        {
            get;
            private set;
        } = 9600;


        public bool IsOpen
        {
            get
            {
                if (_SerialPort == null)
                    return false;
                return true;
            }
        }

        public string Name
        {
            get;
            private set;

        }

        public int ReadBufferSize
        {
            get; set;
        } = 2048;

        public void Send(byte[] datas)
        {
            var dataStream = new DataStream(datas, datas.Length);
            _WriteQueue.Enqueue(dataStream);
            _WriteLock.Set();
        }
        private static byte[] HexStringToBinary(string hexString)
        {
            if (hexString.Length % 2 == 1)
            {
                return null;
            }
            int i = 0;
            int j = 0;
            byte[] ret = new byte[hexString.Length / 2];
            while (i < hexString.Length)
            {
                string hex = string.Empty;
                hex += hexString[i++];
                hex += hexString[i++];
                byte num = Convert.ToByte(hex, 16);
                ret[j++] = num;
            }
            return ret;
        }
        public void Send(string hexString)
        {
            var data = HexStringToBinary(hexString);
            if (data == null)
                throw new Exception("输入的16进制字符串有问题");
            Send(data);
        }

        public void Start(string name, int baudRate)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = GetAvailableComs().FirstOrDefault();
            }
            if (name == null)
                return;
            if (_OpenComs.Where(a => a.Name == name).Count() == 1 && _OpenComs.Where(a => a.Name == name).ToList()[0]._SerialPort != null)
            {
                _SerialPort = _OpenComs.Where(a => a.Name == name).ToList()[0]._SerialPort;
            }
            else
            {
                _SerialPort = new SerialPort(name, baudRate);
                _SerialPort.Open();
                _Running = true;
                _ReadThread = new Thread(new ThreadStart(() =>
                {
                    int exceptionCount = 0;
                    while (_Running)
                    {

                        byte[] data = new byte[ReadBufferSize];
                        int bufflen = 0;

                        try
                        {
                            bufflen = _SerialPort.Read(data, 0, _SerialPort.BytesToRead);
                        }
                        catch (Exception e)
                        {
                            ++exceptionCount;
                            if (exceptionCount > 10)
                                break;
                            continue;
                        }
                        if (bufflen == 0)
                            continue;

                        var package = _Filter.ResolvePackage(data);
                        if (package == null)
                            continue;
                        var cmdId = package.Key;
                        var cmd = CommandManager.Instance.RetrieveCommand(cmdId);
                        if (cmd != null)
                        {
                            _MessageQueue.Enqueue(new Tuple<ICommand, PackageInfo>(cmd, package));
                        }
                    }
                }));
                _WriteThread = new Thread(new ThreadStart(() =>
                {
                    while (_Running)
                    {
                        while (_Running)
                        {
                            while (_WriteQueue.Count > 0)
                            {
                                DataStream ds;
                                _WriteQueue.TryDequeue(out ds);
                                _SerialPort.Write(ds.Data, 0, ds.Data.Length);
                            }
                            _WriteLock.Reset();
                            _WriteLock.WaitOne();
                        }

                        _WriteLock.Reset();
                        _WriteLock.WaitOne();
                    }
                }));
                _MainThread = new Thread(new ThreadStart(() =>
                {
                    while (_Running)
                    {
                        Thread.Sleep(1);
                        for (int i = 0; i < 5; ++i)
                        {
                            Tuple<ICommand, PackageInfo> request;
                            if (_MessageQueue.TryDequeue(out request))
                            {
                                request.Item1.Execute(request.Item2);
                            }
                        }
                    }
                }));
                _ReadThread.Start();
                _WriteThread.Start();
                _MainThread.Start();
            }

        }

        public void Stop()
        {
            _Running = false;
            _WriteLock.Set();
            _MainThread?.Join();
            _WriteThread?.Join();
            _SerialPort?.Close();
            _ReadThread?.Join();
            _SerialPort.Dispose();
            _SerialPort = null;
            _OpenComs.Remove(this);
        }

        public void Dispose()
        {
            Stop();
        }

        public void Send(ISendEntity entity)
        {

            Send(_Encoder.EncodeMessage(entity));
        }
    }
}
