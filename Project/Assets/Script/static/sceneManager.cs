using UnityEngine;
using System.Collections;

public class sceneManager : TempSingletonMonoBehaviour<sceneManager> {

	// メンバ変数
    //--------------------------------------------------------------------
    private string     nextScene;       // 次のシーン
    private fade fade;                  // フェード

	// プロパティ
    //--------------------------------------------------------------------
    public static bool IsFadeOut
    {
        get
        {
            return Instance.fade.enabled;
        }
    }

	// @brief  : シーン読み込み時
    //--------------------------------------------------------------------        
    void Awake()
    {
        // フェードの読み込み
        fade = GetComponentInChildren<fade>();
        
        // なければ作成        
        if(!fade)
        {
            // Resourcesフォルダからプレハブ「fade」を読み込む
            GameObject fadePrefav = (GameObject)Resources.Load ("fade");
            
            // プレハブからインスタンスを生成
            GameObject fadeObj = GameObject.Instantiate(fadePrefav);
            
            // インスタンスを子に置く
            fadeObj.transform.SetParent(transform);
            
            // スクリプトの取得
            fade = fadeObj.GetComponent<fade>();
        }

        // オブジェクトは消えない
        DontDestroyOnLoad(gameObject);
        
        enabled = false;
    }

	// @brief  : 次のシーンへ
    //--------------------------------------------------------------------        
    public static void NextScene( string next )
    {
        if(!Instance) return;
        Instance.SetNextScene(next);
    }

	// @brief  : 更新
    //--------------------------------------------------------------------    
	void Update ()
    {
       // ロード
       if(fade.isNotFade)
       {
            Application.LoadLevel(nextScene);
       }
	}
    
	// @brief  : シーン読み込み終了時
    //--------------------------------------------------------------------
    void OnLevelWasLoaded()
    {
        // 次のシーンのセットをなしにする
        nextScene = null;
        
        // フェードを切る
        fade.enabled = false;

        // 自分も切る
        enabled = false;
    }

	// @brief  : 次のシーンに変更
    //--------------------------------------------------------------------
    private void SetNextScene(string next)
    {
        // 次のシーンがいるなら何もしない
        if(nextScene != null) return ;
        
        // Updateを通す
        enabled = true;
        
        // 次のシーンへ
        nextScene = next;
        
        // フェード開始
        fade.enabled = true;
                
        //Debug.Log("[" + ToString() + "] nextScene = " + nextScene);
    }
}
