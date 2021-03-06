﻿using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    public int DeffenceNum;       // 現在のシールドの耐久度
    public int MaxDeffenceNum;    // 最大のシールド耐久度

    public Texture[] Textures;

    // Use this for initialization
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 弾とシールドの接触判定 玉が当たればシールド耐久度が減る
    /// </summary>
    /// <param name="Col"></param>
    public void TriggerEnter(Collider Col)
    {
        //Debug.Log("Shield Ball");

        DeffenceNum--;

        if (DeffenceNum < MaxDeffenceNum)
        {
            this.GetComponent<MeshRenderer>().material.mainTexture = Textures[1];
        }

        if (DeffenceNum <= 3)
        {
            this.GetComponent<MeshRenderer>().material.mainTexture = Textures[2];
        }

        if (DeffenceNum <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        DeffenceNum = MaxDeffenceNum;

        this.GetComponent<MeshRenderer>().material.mainTexture = Textures[0];
    }
}