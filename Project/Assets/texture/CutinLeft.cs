using UnityEngine;
using System.Collections;

public class CutinLeft : MonoBehaviour
{

    public float CutinTime = 3.0f;
    private float CutinTimeCnt = 0.0f;

    public GameObject CutinBg;
    public GameObject CutinPlayer;
    public GameObject CutinText;

    public float BgScrollX = 0.05f;
    public float BgScrollY = 0.0f;

    private float EndTweenTime = 0.2f;
    private float EndTweenTimeCnt = 0.0f;

    // Use this for initialization
    void Start()
    {
        //背景オブジェクトのUVを念のためリセット
        CutinBg.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", Vector2.zero);
        //BgにTween
        iTween.ScaleTo(CutinBg, iTween.Hash(
                       "scale", new Vector3(25.0f, 17.0f, 1.0f),
                       "time", 0.2,
                       "easeType", "easeOutQuart"));

        //プレイヤーにTween
        iTween.MoveTo(CutinPlayer, iTween.Hash(
                       "position", new Vector3(-5.0f, 0.0f, -3.0f),
                       "time", 0.3,
                       "easeType", "easeOutQuart"));

        //TextにTween
        iTween.ScaleTo(CutinText, iTween.Hash(
                       "scale", new Vector3(1.5f, 1.5f, 1.5f),
                       "time", 0.5,
                       "easeType", "easeOutElastic"));

        // SE再生
        this.gameObject.GetComponent<AudioPlayer>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        //背景スクロール
        float OffsetX = Mathf.Repeat(Time.time * BgScrollX, 1);
        float OffsetY = Mathf.Repeat(Time.time * BgScrollY, 1);
        Vector2 Offset = new Vector2(OffsetX, OffsetY);
        CutinBg.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", Offset);

        //カットインタイムがきたら終わり
        if (CutinTimeCnt < CutinTime)
        {
            CutinTimeCnt += Time.deltaTime;

            if (CutinTimeCnt >= CutinTime)
            {
                End();
            }
        }
        else
        {
            EndTweenTimeCnt += Time.deltaTime;
            if (EndTweenTimeCnt >= EndTweenTime * 2.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void End()
    {
        iTween.Stop(CutinBg);
        iTween.Stop(CutinPlayer);
        iTween.Stop(CutinText);
        //BgにTween
        iTween.ScaleTo(CutinBg, iTween.Hash(
                       "scale", new Vector3(50.0f, 0.0f, 1.0f),
                       "time", EndTweenTime,
                       "easeType", "easeOutQuart"));

        //プレイヤーにTween
        iTween.MoveTo(CutinPlayer, iTween.Hash(
                       "position", new Vector3(20.0f, 0.0f, -3.0f),
                       "time", EndTweenTime,
                       "easeType", "easeOutQuart"));

        //TextにTween
        iTween.ScaleTo(CutinText, iTween.Hash(
                       "scale", new Vector3(0.0f, 0.0f, 0.0f),
                       "time", EndTweenTime,
                       "easeType", "easeInCirc"));
    }
}
