using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;


public enum DisType
{
    Exception,
    Disconnect,
}

public sealed class SocketClient
{
    private TcpClient client = null;
    private MemoryStream memoryStream = null;
    private BinaryReader reader = null;

    private const int MAX_READ = 8192;
    private byte[] byteData = new byte[MAX_READ];

    public SocketClient()
    {
        OnRegister();
    }
    /// <summary>
    /// 注册代理
    /// </summary>
    private void OnRegister()
    {
        memoryStream = new MemoryStream();
        reader = new BinaryReader(memoryStream);
    }
    /// <summary>
    /// 移除代理
    /// </summary>
    public void OnRemoveReg()
    {
        Close();
        reader.Close();
        memoryStream.Close();
    }
    /// <summary>
    /// 建立连接
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    public void ConnectServer(string ip, int port)
    {
        client = null;
        client = new TcpClient(AddressFamily.InterNetwork);
        try
        {
            client.BeginConnect(ip, port, ConnectCallBack, null);
        }
        catch (Exception ex)
        {
            Close();
            Debug.LogError(ex);
        }
    }
    /// <summary>
    /// 连接回调
    /// </summary>
    /// <param name="ir"></param>
    private void ConnectCallBack(IAsyncResult ir)
    {
        Debug.Log("服务器已连接");
        client.EndConnect(ir);
        client.GetStream().BeginRead(byteData, 0, MAX_READ, ReadCallBack, null);
    }
    /// <summary>
    /// 读取回调
    /// </summary>
    /// <param name="ir"></param>
    private void ReadCallBack(IAsyncResult ir)
    {
        int bytesRead = 0;
        try
        {
            lock (client.GetStream())
            {
                bytesRead = client.GetStream().EndRead(ir);
            }
            if (bytesRead < 1)
            {
                Debug.LogError("包体小于1");
                return;
            }
            OnReceive(byteData, bytesRead);//分析数据包内容，抛给逻辑层
            lock (client.GetStream())
            {
                //分析完，再次监听服务器发过来的新消息
                Array.Clear(byteData, 0, byteData.Length);
                client.GetStream().BeginRead(byteData, 0, MAX_READ, ReadCallBack, null);
            }

        }
        catch (Exception ex)
        {
            Close();
            Debug.LogError("读取错误--" + ex);
        }
    }
    /// <summary>
    /// 处理字节数据
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="length"></param>
    private void OnReceive(byte[] bytes, int length)
    {
        memoryStream.Seek(0, SeekOrigin.End);
        memoryStream.Write(bytes, 0, length);
        memoryStream.Seek(0, SeekOrigin.Begin);
        while (RemainingBytes() > 4)
        {
            int msglen = reader.ReadUInt16();
            int protoId = reader.ReadUInt16();
            Debug.Log("收到消息号---" + protoId);
            if (RemainingBytes() >= msglen)
            {
                if (ProtoDic.isContainProtoId(protoId))
                {
                    Type protoType = ProtoDic.GetProtoTypeByProtoId(protoId);
                    using (MemoryStream ms = new MemoryStream(reader.ReadBytes(msglen)))
                    {
                        object o = ProtoBuf.Meta.RuntimeTypeModel.Default.Deserialize(ms, null, protoType);
                        NetManager.Instance.DispatchProto(protoId, o);
                    }
                }
                else
                {
                    reader.ReadBytes(msglen);
                    Debug.LogError("消息号--"+protoId+"--不存在");
                }
            }
            else
            {
                memoryStream.Position -= 4;
                break;
            }
        }
        byte[] leftover = reader.ReadBytes((int)RemainingBytes());
        memoryStream.SetLength(0);//这句必须有
        memoryStream.Write(leftover, 0, leftover.Length);
    }
    /// <summary>
    /// 剩余字节数
    /// </summary>
    /// <returns></returns>
    private long RemainingBytes()
    {
        return memoryStream.Length - memoryStream.Position;
    }

    /// <summary>
    /// 写数据
    /// </summary>
    /// <param name="msg"></param>
    private void WriteMessage(byte[] msg)
    {
        if (client != null && client.Connected)
        {
            memoryStream.BeginWrite(msg, 0, msg.Length, WriteCallBack, null);
        }
        else
        {
            Debug.LogError("连接已关闭");
        }
    }
    /// <summary>
    /// 写回调
    /// </summary>
    /// <param name="ir"></param>
    void WriteCallBack(IAsyncResult ir)
    {
        try
        {
            memoryStream.EndWrite(ir);
        }
        catch (Exception ex)
        {
            Debug.LogError("WriteCallBack-" + ex);
        }

    }
    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="buffer"></param>
    public void SendMessage(ByteBuffer buffer)
    {
        WriteMessage(buffer.ToBytes());
        buffer.Close();
    }

    public void Close()
    {
        if (client != null)
        {
            client.Close();
            client = null;
        }
    }

}
