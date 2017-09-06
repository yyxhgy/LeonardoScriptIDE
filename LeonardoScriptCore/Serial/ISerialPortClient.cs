using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeonardoScriptCore.Serial
{
    /// <summary>
    /// 串口客户端接口
    /// </summary>
    public interface ISerialPortClient
    {
        /// <summary>
        /// 串口名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 波特率，默认9600
        /// </summary>
        int BaudRate { get; }
        /// <summary>
        /// 连接状态
        /// </summary>
        bool IsOpen { get; }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="datas"></param>
        void Send(Byte[] datas);
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="entity"></param>
        void Send(ISendEntity entity);
        /// <summary>
        /// 发送16进制字符串
        /// </summary>
        /// <param name="hexString"></param>
        void Send(string hexString);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        //event EventHandler<DataStream> OnDataRecieved;
        /// <summary>
        /// 端口断开事件
        /// </summary>
        event EventHandler OnSessionClosed;
        /// <summary>
        /// 开启端口
        /// </summary>
        /// <param name="name"></param>
        /// <param name="baudRate"></param>
        void Start(string name, int baudRate);
        /// <summary>
        /// 停止端口
        /// </summary>
        void Stop();
    }
}
