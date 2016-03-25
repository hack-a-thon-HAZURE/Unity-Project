using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    public int DeffenceNum;       // 現在のシールドの耐久度
    public int MaxDeffenceNum;    // 最大のシールド耐久度

    // Use this for initialization
    void Start()
    {
        DeffenceNum = MaxDeffenceNum;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 弾とシールドの接触判定 玉が当たればシールド耐久度が減る
    /// </summary>
    /// <param name="Col"></param>
    void OnCollisionEnter(Collision Col)
    {
        Debug.Log("Shield Ball");

        DeffenceNum--;

        if (DeffenceNum <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}