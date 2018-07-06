using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Login;

public sealed class ProtoDic {

    /// <summary>
    /// 协议号与协议
    /// </summary>
    private static Dictionary<int, Type> dic = new Dictionary<int, Type>
    {
        {1001,typeof(ReqLogin) },
        {1002,typeof(RespLogin) },
        {1004,typeof(RespGuoLai) },



    };

    public static Type GetProtoTypeByProtoId(int protoId)
    {
        Type type = null;
        dic.TryGetValue(protoId, out type);
        return type;
    }
    public static int GetProtoIdByProtoType(Type type)
    {
        foreach (var item in dic)
        {
            if (item.Value == type)
                return item.Key;
        }
        return -1;
    }
    public static bool isContainProtoId(int protoId)
    {
        return dic.ContainsKey(protoId);
    }
    public static bool isContainProtoType(Type type)
    {
       return  dic.ContainsValue(type);
    }

}
