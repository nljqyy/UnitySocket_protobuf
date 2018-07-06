using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class myLogin : SocketMessageBase {

    public override void InitAddTocHandler()
    {
        AddTocHandler(typeof(Login.RespLogin), Handler);
    }

    private void Handler(object o)
    {
        Login.RespLogin rsp = o as Login.RespLogin;
        Debug.Log("isWin----"+rsp.isWin+"---level---"+rsp.level);
    }

    public static void Send()
    {
        Login.ReqLogin lg = new Login.ReqLogin();
        lg.username = "张三";
        lg.account = "158318";
        lg.password = "123456";
        SendTos(lg);
    }
}
