/**************************************************************************************************

 @File   : [ ForceBallSpeed ] 
 @Auther : Ayumi Yasui

**************************************************************************************************/
using UnityEngine;
using System.Collections;

public class ForceBallSpeed : MonoBehaviour {

    /// メンバ変数
    public GameObject BuestEffectPrefab;
    public GameObject CutinPrefab;
    public GameObject MegaHitPrefab;

    public float HoldTime;  // 捕まえておく時間
    public float AddSpeed;  // 追加する速さ

    private Ball  ball;      // ボール
    private bool  is_catch;  // ボールを捕まえた
    private float hold_time_count;  // 捕まえておくまでの時間

    /// プロパティ
    public bool IsCatch { get { return is_catch; } }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        ball            = null;
        is_catch = false;
        hold_time_count = HoldTime;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (!IsCatch) return;
        hold_time_count -= Time.deltaTime;
        if (hold_time_count < 0.0f) EndCatch();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void OnTriggerEnter(Collider coll)
    {
        if (ball) Debug.Log("Catch Ready ?");
        if (IsCatch) return;

        // ボールスクリプトの取得
        ball = coll.GetComponent<Ball>();
        if (ball) Debug.Log("Catch Ready");
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void OnTriggerExit(Collider coll)
    {
        if (IsCatch) return;

        // ボールスクリプトの取得
        if(ball) if (ball.gameObject == coll.gameObject) ball = null;
        if (!ball) Debug.Log("Catch End");
    }

    /// <summary>
    /// キャッチ開始
    /// </summary>
    public void StartCatch()
    {
        Debug.Log("Try Catch");

        if (IsCatch) return;
        if (!ball) return;
        enabled = true;

        is_catch = true;
        hold_time_count = HoldTime;

        ball.enabled = false;
        ball.gameObject.transform.parent = transform;

        // 速さの追加
        var vec   = ball.Vec.normalized;
        var speed = ball.Vec.magnitude;

        speed += AddSpeed;
        vec   *= -speed;

        ball.Vec = vec;

        // エフェクト
        GameObject.Instantiate(CutinPrefab);

        Debug.Log("Catch");
    }

    /// <summary>
    /// キャッチ終了
    /// </summary>
    private void EndCatch()
    {
        if (!IsCatch) return;
        //enabled = false;

        is_catch = false;
        hold_time_count = HoldTime;

        var effect = GameObject.Instantiate(BuestEffectPrefab);
        effect.transform.position = ball.gameObject.transform.position;

        effect = GameObject.Instantiate(MegaHitPrefab);
        effect.transform.position = ball.gameObject.transform.position;

        ball.enabled = true;
        ball.gameObject.transform.parent = null;
        ball = null;

    }
}
//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//