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
        _GameRuleManager = GameObject.Find("/GameRuleManager").GetComponent<GameRuleManager>();

        if (Application.loadedLevelName == "Pong")
        {
            _EvolutionManager = GameObject.Find("EvolutionManager").GetComponent<EvolutionManager>();
        }
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update () 
    {

    }

    // 弾とゴールの接触判定
    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag != "Ball") return;
        Debug.Log("Goal");

        _GameRuleManager.DoneGoal(OwnerID);
        Destroy(Col.gameObject);

        if (Application.loadedLevelName == "Pong")
        {
            _EvolutionManager.ResetCount();
        }
    }
}

//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//