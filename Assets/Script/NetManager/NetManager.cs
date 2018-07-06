using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

public class NetManager : MonoBehaviour
{
    private const string ip = "192.168.15.162";
    private const int port = 7788;
    private Dictionary<Type, Action<object>> handlerDic;
    private Queue<KeyValuePair<Type, object>> sEvents;

    public static NetManager Instance { get; private set; }
    public SocketClient socketClient { get; private set; }

    private void Awake()
    {
        Instance = this;
        socketClient = new SocketClient();
        sEvents = new Queue<KeyValuePair<Type, object>>();
        handlerDic = new Dictionary<Type, Action<object>>();
        Proto_Object.Init();
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        SendConnect();
    }
    /// <summary>
    /// 链接服务器
    /// </summary>
    public void SendConnect()
    {
        socketClient.ConnectServer(ip, port);
    }
    /// <summary>
    /// 关闭连接
    /// </summary>
    public void Close()
    {
        socketClient.OnRemoveReg();
    }
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="obj"></param>
    public void SendMessage(object obj)
    {
        if (!ProtoDic.isContainProtoType(obj.GetType()))
        {
            Debug.LogError("协议类型没有注册");
            return;
        }
        ByteBuffer buff = new ByteBuffer();
        int protoId = ProtoDic.GetProtoIdByProtoType(obj.GetType());
        using (MemoryStream ms = new MemoryStream())
        {
            ProtoBuf.Meta.RuntimeTypeModel.Default.Serialize(ms, obj);
            byte[] msg = ms.ToArray();
            buff.WriteShort((ushort)ms.Length);//消息长度
            buff.WriteShort((ushort)protoId);//消息号
            buff.WriteBytes(msg);//消息
            socketClient.SendMessage(buff);
        }
    }
    /// <summary>
    /// 处理服务器发过来的消息
    /// </summary>
    /// <param name="protoId"></param>
    /// <param name="obj"></param>
    public void DispatchProto(int protoId, object obj)
    {
        Type protoType = ProtoDic.GetProtoTypeByProtoId(protoId);
        try
        {
            sEvents.Enqueue(new KeyValuePair<Type, object>(protoType, obj));
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }

    private void Update()
    {
        if (sEvents.Count > 0)
        {
            while (sEvents.Count>0)
            {
                KeyValuePair<Type, object> _event = sEvents.Dequeue();
                if (handlerDic.ContainsKey(_event.Key))
                {
                    handlerDic[_event.Key](_event.Value);
                }
                else
                {
                    Debug.LogError("类型-"+ _event.Key +"-没有注册事件");
                }
            }
        }
    }
    /// <summary>
    /// 注册处理事件
    /// </summary>
    /// <param name="type">协议类型</param>
    /// <param name="action"></param>
    public void AddHandler(Type type,Action<object> action)
    {
        if (!handlerDic.ContainsKey(type))
        {
            handlerDic.Add(type,action);
        }
    }
}

