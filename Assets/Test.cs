using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int num = 123456;
        int num2 = 234;
        byte[] temp = new byte[50];
        byte[] data= BitConverter.GetBytes(num);
        Array.Copy(data, temp, data.Length);
        data= BitConverter.GetBytes(num2);
        Array.Copy(data, 0,temp,4,data.Length);


        int big= BitConverter.ToInt32(temp.ToList().GetRange(0, 4).ToArray(), 0);
        int big2 = BitConverter.ToInt32(temp.ToList().GetRange(4, 4).ToArray(), 0);

        data = System.Text.Encoding.UTF8.GetBytes("我");

        Debug.Log("data--"+data.Length);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
