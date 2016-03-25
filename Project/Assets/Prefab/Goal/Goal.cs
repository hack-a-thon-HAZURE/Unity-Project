/**************************************************************************************************

 @File   : [ Goal ] 
 @Auther : Unisawa

**************************************************************************************************/
using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    public int OwnerID;

    private GameRuleManager _GameRuleManager;

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
        _GameRuleManager = GameObject.Find("/GameRuleManager").GetComponent<GameRuleManager>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update () 
    {

    }

    // 弾とゴールの接触判定
    void OnCollisionEnter(Collision Col)
    {
        Debug.Log("Goal");

        _GameRuleManager.DoneGoal(OwnerID, Col.gameObject);
    }
}

//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//