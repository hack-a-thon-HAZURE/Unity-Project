using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(AudioSource))]
public class AudioPlaySEButton : MonoBehaviour
{
    private AudioSource Source;

    void Start()
    {
        // [AudioSource] のアタッチのし忘れを防止する
        Source = this.gameObject.GetComponent<AudioSource>();

        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            Source.Play();
        });
    }
}
