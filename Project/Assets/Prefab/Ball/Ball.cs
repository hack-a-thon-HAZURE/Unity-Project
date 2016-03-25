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
        int Dir = (Random.Range(0, 2) == 0 ? -1 : 1);

        this.GetComponent<Rigidbody>().AddForce(Dir * transform.right * BallSpeed, ForceMode.VelocityChange);
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