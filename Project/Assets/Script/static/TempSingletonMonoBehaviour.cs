using UnityEngine;

public static class SingleGameObject
{
	// グローバル変数
    //--------------------------------------------------------------------
    private static GameObject instance = null;
    // プロパティ
    //--------------------------------------------------------------------
    static public GameObject Instance // Singleton GameObject
    {
        get
        {
            // インスタンスがあるならそのオブジェクトを返す
            if(instance) return instance;
            
            string objName = "Singleton";

            // オブジェクトの発見
            instance = GameObject.Find(objName);
            if(instance) return instance;
            
            // オブジェクトの作成
            instance = new GameObject(objName);
            instance.isStatic = true;
            
            // オブジェクトは消えない
            GameObject.DontDestroyOnLoad(instance);
            
            // 作成したオブジェクトを返す
            return instance;
        }
    }
}

public class TempSingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

	// グローバル変数
    //--------------------------------------------------------------------
    private static T instance = null;       // 唯一のインスタンス
    private static bool quitApp = false;    // アプリケーション終了
    
    // プロパティ
    //--------------------------------------------------------------------
	public static T Instance   // 唯一のインスタンス
    {
        get
        {
            // アプリケーションが終了しているなら返さない
            if(quitApp) return null;

            // インスタンスがあるなら返す
            if(instance) return instance;

            // Singletonオブジェクトから取得
            if(SingleGameObject.Instance) instance = SingleGameObject.Instance.GetComponent<T>();
            if(instance) return instance;

            // シングルトンオブジェクトにくっつける
            instance = SingleGameObject.Instance.AddComponent<T>();

            return instance;
        }
    }

    // @brief  : アプリケーション終了時にオブジェクトを破棄する
    //--------------------------------------------------------------------    
	void OnApplicationQuit()
    {
        // アプリケーション終了
        quitApp = true;

        // ゲームオブジェクトがあれば破棄
		if(gameObject) Destroy(gameObject);
	}
}
