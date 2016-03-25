using UnityEngine;
using UnityEngine.UI;

public class debugSceneChenge : MonoBehaviour {

	// メンバ変数
	readonly string[] SceneName =	// シーン名
	{
		"Title","Main","Result"
	};

	// 初期化
	void Awake ()
	{
		Dropdown menu = GetComponent<Dropdown>();
		menu.value = Application.loadedLevel;
	}
	
	// ドロップダウンの選択
	// 参考： http://tsubakit1.hateblo.jp/entry/2015/11/09/042319
	public void OnValueChanged(int value)
	{
		if(Application.loadedLevel == value) return;
		sceneManager.NextScene(SceneName[value]);
	}
}
