using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class AudioVolumeSlider : MonoBehaviour
{
    public bool BGMType = true;
    public bool SEType;

    // Use this for initialization
    void Start()
    {
        // オーディオの現在の値を取得し、スライダーの値を適正な値へ移動させる
        AudioMixer Mixer = GameObject.Find("/AudioManager").GetComponent<AudioManager>().Mixer;

        float MaxVolume;

        if(BGMType)
            Mixer.GetFloat("BGMVolume", out MaxVolume);
        else if(SEType)
            Mixer.GetFloat("SEVolume", out MaxVolume);
        else
            Mixer.GetFloat("VOICEVolume", out MaxVolume);

        MaxVolume = MaxVolume / 80 + 1;

        Debug.Log(MaxVolume);
        this.GetComponent<Slider>().value = MaxVolume;
    }
}