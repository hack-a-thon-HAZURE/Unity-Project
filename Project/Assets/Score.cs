/**************************************************************************************************

 @File   : [ Score ] 
 @Auther : Unisawa

**************************************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
    public int ScoreNum;

    private Text ScoreText;

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
        ScoreNum = 0;

        ScoreText = GetComponent<Text>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update () 
    {
        ScoreText.text = ScoreNum.ToString();
    }
}

//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//