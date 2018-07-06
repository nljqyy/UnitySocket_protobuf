using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public abstract class SocketMessageBase
{
    public abstract void InitAddTocHandler();

    protected void AddTocHandler(Type type, Action<object> handler)
    {
       NetManager.Instance.AddHandler(type, handler);
    }

    protected  static void SendTos(object obj)
    {
       NetManager.Instance.SendMessage(obj);
    }
}
