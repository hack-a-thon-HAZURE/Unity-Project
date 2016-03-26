using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    public AudioSource Source;

    void Start()
    {
        // [AudioSource] のアタッチのし忘れを防止する
        Source = this.gameObject.GetComponent<AudioSource>();
    }

    public void OnEnable()
    {
        AudioManager.Instance.RegistPlayer(this);
    }

    public void OnDisable()
    {
        Source.Stop();

        if(AudioManager.Instance != null)
            AudioManager.Instance.UnregistPlayer(this);
    }

    /// <summary>
    /// サウンドの再生を行う
    /// </summary>
    public void Play()
    {
        Source.Play();
    }

    /// <summary>
    /// サウンド停止
    /// </summary>
    public void Stop()
    {
        Source.Stop();
    }

    /// <summary>
    /// サウンド一時停止
    /// </summary>
    public void Pause()
    {
        Source.Pause();
    }
}