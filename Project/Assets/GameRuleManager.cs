/**************************************************************************************************

 @File   : [ GameRuleManager ] 
 @Auther : Unisawa

**************************************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameRuleManager : MonoBehaviour
{
    public GameObject BallObject;

    public Score ScoreOne;
    public Score ScoreTwo;

    public int MaxScoreNum;

    // 現在カットイン中
    private static bool isCutin;

    public static bool IsCutIn
    {
        set
        {
            isCutin = value;
        }
        get
        {
            return isCutin;
        }
    }

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
        isCutin = false;
        Instantiate(BallObject);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update () 
    {

    }

    public void DoneGoal(int OwnerID)
    {
        // ゴールしたプレイヤーを特定する
        // OwnerIDはゴールの所持者(敵)
        Score GoalPlayer = ((OwnerID == 1) ? ScoreTwo : ScoreOne);

        GoalPlayer.ScoreNum += 1;

        if (GoalPlayer.ScoreNum < MaxScoreNum)
        {
            AudioPlayer _AudioPlayer = gameObject.GetComponent<AudioPlayer>();
            _AudioPlayer.Play();

            StartCoroutine("Restart");

        }else
        {
            StartCoroutine("GameEnd");
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.0f);

        Instantiate(BallObject);
    }

    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("Main");
    }

    public void StartCutin()
    {
        isCutin = true;
    }
}

//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//