using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public sealed class Proto_Object{

    private static List<SocketMessageBase> list = new List<SocketMessageBase>
    {
        new myLogin(),

    };

    public static void Init()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != null)
            {
                list[i].InitAddTocHandler();
            }
        }
    }
}
