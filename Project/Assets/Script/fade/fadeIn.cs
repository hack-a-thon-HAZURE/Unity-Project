using UnityEngine;
using UnityEngine.UI;

public class fadeIn : MonoBehaviour {

	// メンバ変数
    //--------------------------------------------------------------------
    private fade master;            // フェードスクリプト
    private GraphicRaycaster ray;   // レイキャスト
    
	// @brief  : シーン開始時
    //--------------------------------------------------------------------
    void Awake()
    {
        master = GetComponent<fade>();
        ray    = GetComponent<GraphicRaycaster>();
        enabled = false;
    }
    
	// @brief  : 初期化
    //--------------------------------------------------------------------
    void OnEnable()
    {
        ray.enabled = true;
        master.alpha = 0.0f;
    }

	// @brief  : 更新
    //--------------------------------------------------------------------
    void Update()
    {
        // アルファを足す
        master.alpha += master.GetAddAlpha() * Time.deltaTime;
        
        // 完了したら更新しない
        if(master.alpha >= 1.0f) enabled = false;
    }
}
