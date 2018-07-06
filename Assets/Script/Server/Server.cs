using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.IO;
using System;

public class Server : MonoBehaviour
{

    //1.声明一个listener:套接字和接口都绑定好了
    TcpListener listener = new TcpListener(IPAddress.Parse("192.168.15.162"), 7788);
    private void Start()
    {
        //2.开始监听
        listener.Start();
        //3.等待客户接入
        listener.BeginAcceptTcpClient(AcceptClient, listener);
    }

    private void AcceptClient(IAsyncResult ir)
    {
        TcpListener listener = ir.AsyncState as TcpListener;
        TcpClient tcpClient = listener.EndAcceptTcpClient(ir);
        //4.创建一个流用来收发数据
        NetworkStream stream = tcpClient.GetStream();
        //读入,也就是接受一个数据
        byte[] data = new byte[1024];
        Login.RespLogin rsp = new Login.RespLogin();
        rsp.isWin = true;
        rsp.level = 5;
        MemoryStream ms = new MemoryStream();
        ByteBuffer buff = new ByteBuffer();
        ProtoBuf.Meta.RuntimeTypeModel.Default.Serialize(ms, rsp);
        data = ms.ToArray();
        buff.WriteShort((ushort)ms.Length);
        buff.WriteShort((ushort)1002);
        buff.WriteBytes(data);
        stream.Write(buff.ToBytes(), 0, buff.ToBytes().Length);
        ms.Close();
        //5.关闭相应的流和监听器
        stream.Close();
        listener.BeginAcceptTcpClient(AcceptClient, listener);
    }


}
