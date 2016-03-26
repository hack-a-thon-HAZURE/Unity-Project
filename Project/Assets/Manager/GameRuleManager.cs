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
    public GameObject ResultPrefab;

    public Score ScoreOne;
    public Score ScoreTwo;

    public int MaxScoreNum;

    public GameObject ShieldObject000;
    public GameObject ShieldObject001;

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

        AudioManager.Instance.BGMPlay("drumnloop");

        if (Application.loadedLevelName == "Pong")
        {
            gameObject.GetComponent<AudioPlayer>().Source = gameObject.GetComponents<AudioSource>()[0];
        }

        if (Application.loadedLevelName == "Main")
        {
            gameObject.GetComponent<AudioPlayer>().Source = gameObject.GetComponents<AudioSource>()[1];
        }
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
            if (Application.loadedLevelName == "Pong")
            {
                AudioManager.Instance.BGMSource.volume = 0.0f;
            }
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

        if (Application.loadedLevelName == "Main")
        {
            ShieldObject000.SetActive(true);
            ShieldObject000.GetComponent<Shield>().Reset();

            ShieldObject001.SetActive(true);
            ShieldObject001.GetComponent<Shield>().Reset();
        }

        Instantiate(BallObject);
    }

    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(1.0f);

        AudioManager.Instance.BGMSource.volume = 0.7f;
        GameObject.Instantiate(ResultPrefab);
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