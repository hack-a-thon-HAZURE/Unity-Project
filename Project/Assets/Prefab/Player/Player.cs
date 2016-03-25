/**************************************************************************************************

 @File   : [ Player ] 
 @Auther : Unisawa

**************************************************************************************************/
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private EvolutionManager _EvolutionManager;

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
        _EvolutionManager = GameObject.Find("EvolutionManager").GetComponent<EvolutionManager>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update () 
    {

    }

    /// <summary>
    /// 玉とプレイヤーの当たり判定を取って、進化フラグを
    /// </summary>
    /// <param name="Col"></param>
    void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag != "Ball") return;

        Debug.Log("進化フラグ +1");

        _EvolutionManager.AddCount();
    }
}

//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//