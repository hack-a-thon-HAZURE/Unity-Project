using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    public AudioMixer Mixer;

    public AudioMixerGroup MixerBgmGroup;
    public AudioMixerGroup MixerSeGroup;
    public AudioMixerGroup MixerVoiceGroup;

    // 現在流れているBGMソース
    public AudioSource BGMSource;

    /// <summary>
    /// BGM切り替え時間
    /// </summary>
    public float m_ShiftTime = 3.0f;

    private float  m_FadeVolume;       // フェード時の音量
    private float  m_FadeTime;         // フェード経過時間
    private bool   m_IsShifting;       // BGM切り替え中かどうか
    private string m_NextBGMName;      // 切り替えるBGMの名前

    /// <summary>
    /// 全オーディオ情報
    /// </summary>
    public List<AudioPlayer> AudioPlayers
    {
        get;
        private set;
    }

    /// <summary>
    /// 全BGM情報
    /// </summary>
    public List<AudioPlayer> AudioBgmPlayers
    {
        get
        {
            return AudioPlayers.FindAll(Audioplayer => Audioplayer.Source.outputAudioMixerGroup == MixerBgmGroup);
        }
    }

    /// <summary>
    /// 全SE情報
    /// </summary>
    public List<AudioPlayer> AudioSePlayers
    {
        get
        {
            return AudioPlayers.FindAll(Audioplayer => Audioplayer.Source.outputAudioMixerGroup == MixerSeGroup);
        }
    }

    /// <summary>
    /// 全VOICE情報
    /// </summary>
    public List<AudioPlayer> AudioVoicePlayers
    {
        get
        {
            return AudioPlayers.FindAll(Audioplayer => Audioplayer.Source.outputAudioMixerGroup == MixerVoiceGroup);
        }
    }

    /// <summary>
    /// 全BGM(AudioClip)のリスト
    /// 実行時に Resources/Audio/BGM フォルダから自動で読み込む
    /// </summary>
    private Dictionary<string, AudioClip> AudioClipDict = null;

    /// <summary>
    /// コルーチン中断に使用
    /// </summary>
    private IEnumerator m_FadeInCoroutine;

    /// <summary>
    /// コルーチン中断に使用
    /// </summary>
    private IEnumerator m_FadeOutCoroutine;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AudioManager()
    {
        AudioPlayers = new List<AudioPlayer>();
    }

    /// <summary>
    /// インスタンスの生成
    /// </summary>
    void Awake()
    {
        if(this != Instance)
        {
            Destroy(this.gameObject);

            return;
        }

        DontDestroyOnLoad(this.gameObject);

        // [ Resources/Audio/BGM ] フォルダからBGMを探す
        AudioClipDict = new Dictionary<string, AudioClip>();
        foreach(AudioClip LoadBGM in Resources.LoadAll<AudioClip>("Audio/BGM"))
        {
            AudioClipDict.Add(LoadBGM.name, LoadBGM);
        }
    }

    /// <summary>
    /// 初期設定
    /// </summary>
    void Start()
    {
        BGMSource = this.gameObject.GetComponent<AudioSource>();

        m_IsShifting = false;
    }

    /// <summary>
    /// サウンドプレイヤーの登録
    /// </summary>
    /// <param name="Audioplayer">対象のサウンド</param>
    public void RegistPlayer(AudioPlayer Audioplayer)
    {
        AudioPlayers.Add(Audioplayer);
    }

    /// <summary>
    /// サウンドプレイヤーの解除
    /// </summary>
    /// <param name="Audioplayer">対象のサウンド</param>
    public void UnregistPlayer(AudioPlayer Audioplayer)
    {
        AudioPlayers.Remove(Audioplayer);
    }

    /// <summary>
    /// BGMの再生
    /// </summary>
    /// <param name="PlayBGMName"></param>
    public void BGMPlay(string PlayBGMName)
    {
        // 既にBGM切り替え中かどうか
        if(m_IsShifting)
            return;

        if(!AudioClipDict.ContainsKey(PlayBGMName))
        {
            Debug.LogError(string.Format("BGM名[{0}]がありません", PlayBGMName));

            return;
        }

        // すでに指定されたBGMを再生しているか確認
        if((BGMSource != null) && (BGMSource.clip == AudioClipDict[PlayBGMName]))
        {
            Debug.Log("対象のBGMは現在再生中: " + PlayBGMName);

            return;
        }

        //再生中のBGMをフェードアウト
        BGMStop();

        m_NextBGMName = PlayBGMName;
        m_IsShifting = true;
    }

    /// <summary>
    /// 現在流しているBGMを最初から流し直す
    /// </summary>
    public void BGMRestart(string PlayBGMName)
    {
        // 既にBGM切り替え中かどうか
        if (m_IsShifting)
            return;

        if (!AudioClipDict.ContainsKey(PlayBGMName))
        {
            Debug.LogError(string.Format("BGM名[{0}]がありません", PlayBGMName));

            return;
        }

        // すでに指定されたBGMを再生しているか確認
        if ((BGMSource != null) && (BGMSource.clip != AudioClipDict[PlayBGMName]))
        {
            Debug.Log("対象のBGMは現在再生していない: " + PlayBGMName);

            return;
        }

        //再生中のBGMをフェードアウト
        BGMStop();

        m_NextBGMName = PlayBGMName;
        m_IsShifting = true;
    }

    /// <summary>
    /// BGMを停止、フェードアウトする
    /// </summary>
    public void BGMStop()
    {
        if(BGMSource != null)
        {
            m_FadeOutCoroutine = FadeOutCoroutine(BGMSource, BGMSource.volume);
            StartCoroutine(m_FadeOutCoroutine);
        }
    }

    /// <summary>
    /// BGMをただちに停止します。
    /// </summary>
    public void StopImmediately()
    {
        m_FadeInCoroutine  = null;
        m_FadeOutCoroutine = null;

        BGMSource = null;
    }

    /// <summary>
    /// フェードイン時の処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeInCoroutine(AudioSource BGM, float FitstVolume)
    {
        float MaxVolume;
        Mixer.GetFloat("BGMVolume", out MaxVolume);

        // フェードイン
        while(m_FadeTime <= m_ShiftTime)
        {
            m_FadeVolume = Mathf.Lerp(FitstVolume, MaxVolume, m_FadeTime / m_ShiftTime);
            m_FadeTime += Time.deltaTime;
            BGM.volume = m_FadeVolume;

            yield return null;
        }

        m_FadeTime = 0.0f;
        m_FadeVolume = 1.0f;
        BGM.volume = m_FadeVolume;
        m_IsShifting = false;

        Debug.Log("BGM FadeIn終了");
    }

    /// <summary>
    /// フェードアウト時の処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOutCoroutine(AudioSource BGM, float FitstVolume)
    {
        // フェードアウト
        while(m_FadeTime <= m_ShiftTime)
        {
            m_FadeVolume = Mathf.Lerp(FitstVolume, 0.0f, m_FadeTime / m_ShiftTime);
            m_FadeTime += Time.deltaTime;
            BGM.volume = m_FadeVolume;

            yield return null;
        }

        m_FadeTime = 0.0f;
        m_FadeVolume = 0.0f;
        BGM.volume = m_FadeVolume;

        Debug.Log("BGM FadeOut終了");

        // BGM切り替え処理なら、フェードインを行う
        if(m_IsShifting)
        {
            // BGM再生開始
            BGMSource.clip = AudioClipDict[m_NextBGMName];
            BGMSource.Play();

            m_FadeInCoroutine = FadeInCoroutine(BGMSource, BGMSource.volume);
            StartCoroutine(m_FadeInCoroutine);
        }
    }

    /// <summary>
    /// フェードイン処理を中断
    /// </summary>
    private void StopFadeIn()
    {
        if(m_FadeInCoroutine != null)
            StopCoroutine(m_FadeInCoroutine);

        m_FadeInCoroutine = null;

    }

    /// <summary>
    /// フェードアウト処理を中断
    /// </summary>
    private void StopFadeOut()
    {
        if(m_FadeOutCoroutine != null)
            StopCoroutine(m_FadeOutCoroutine);

        m_FadeOutCoroutine = null;
    }
}