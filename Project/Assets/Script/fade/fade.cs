using UnityEngine;
using UnityEngine.UI;

public class fade : MonoBehaviour {

	// メンバ変数
    //--------------------------------------------------------------------
    readonly public float fadeTime = 1.0f;     // フェード時間
    private Image image;                // テクスチャイメージ
    private fadeIn  fadeIn;             // フェードイン
    private fadeOut fadeOut;            // フェードアウト
    private float addAlpha;             // 毎時足すアルファ

	// プロパティ
    //--------------------------------------------------------------------
    public float alpha      // イメージのアルファを変更
    {
        set
        {
            Color color = image.color;
            color.a = value;
            image.color = color;
        }
        get
        {
            if(image) return image.color.a;
            return 0.0f;
        }

    }
    public bool isNotFade  // フェードしていない
    {
        get
        {
            return !fadeIn.enabled && !fadeOut.enabled;
        }
    }

	// @brief  : シーン読み込み時
    //--------------------------------------------------------------------
    void Awake()
    {
        // イメージコンポーネントの取得
        image = GetComponent<Image>();
        
        // スクリプトの取得
        fadeIn  = GetComponent<fadeIn>();
        fadeOut = GetComponent<fadeOut>();
        
        // 毎時足すアルファ値の計算
        addAlpha = 1.0f / fadeTime;
        
        // アルファは0
        alpha = 0.0f;
        
        // レイキャストを切る
        GetComponent<GraphicRaycaster>().enabled = false;
    }

	// @brief  : アクティブ時(フェードイン)
    //--------------------------------------------------------------------
    void OnEnable()
    {
        fadeOut.enabled = false;
        fadeIn.enabled  = true;
    }

	// @brief  : アクティブではない時(フェードアウト)
    //--------------------------------------------------------------------
    void OnDisable()
    {
        fadeIn.enabled  = false;
        fadeOut.enabled = true;
    }

	// @brief  : 毎フレーム変化する量
    //--------------------------------------------------------------------
    public float GetAddAlpha()
    {
        return addAlpha;
    }
}
