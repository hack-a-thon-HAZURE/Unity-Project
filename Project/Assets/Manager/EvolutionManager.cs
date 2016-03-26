using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EvolutionManager : MonoBehaviour
{
    [SerializeField]
    private int EvolutionCount;

    public int MaxEvolutionCount;    // モードチェンジ

    // Use this for initialization
    void Start()
    {
        EvolutionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCount()
    {
        if (Application.loadedLevelName != "Pong") return;

        AudioManager.Instance.BGMSource.volume += 0.1f;
        EvolutionCount++;

        if (EvolutionCount >= MaxEvolutionCount)
        {
            Debug.Log("Evolution!");

            // 画面遷移
            sceneManager.NextScene("Main");
            AudioManager.Instance.BGMPlay("hurryup");
        }
    }

    public void ResetCount()
    {
        if (Application.loadedLevelName != "Pong") return;

        EvolutionCount = 0;
    }
}