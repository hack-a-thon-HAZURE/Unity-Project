using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class AudioPlayButton : MonoBehaviour
{
    public string PlayBGMName;

    // Use this for initialization
    void Start()
    {
        // ボタン押下時の処理を設定する
        AudioManager _AudioManager = GameObject.Find("/AudioManager").GetComponent<AudioManager>();

        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            _AudioManager.BGMPlay(PlayBGMName);
        });
    }
}
