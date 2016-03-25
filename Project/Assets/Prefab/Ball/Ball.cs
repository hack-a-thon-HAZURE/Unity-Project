/**************************************************************************************************

 @File   : [ Ball ] 
 @Auther : Unisawa

**************************************************************************************************/
using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public float BallSpeed;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {

    }

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start () 
    {
        // ボールが飛ぶ方向(左右)をランダムで決めている
        int LR = (Random.Range(0, 2) == 0 ? -1 : 1);
        float RotZ = Random.Range(Mathf.PI/4.0f, -Mathf.PI /4.0f);

        // ベクトルの生成
        Vector3 DirNor = transform.right;
        DirNor *= LR;

        Vector3 Dir = new Vector3();
        Dir.x =  Mathf.Cos(RotZ) * DirNor.x + Mathf.Sin(RotZ) * DirNor.y;
        Dir.y = -Mathf.Sin(RotZ) * DirNor.x + Mathf.Cos(RotZ) * DirNor.y;

        this.GetComponent<Rigidbody>().AddForce(Dir * BallSpeed, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update () 
    {

    }
}

//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//