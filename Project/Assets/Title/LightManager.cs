using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour 
{
	public Light Light;                  //ライトオブジェトを指定

	private float time = 0.0f;
	private int cnt = 0;


	void Start(){

	}

	void Update () {
		cnt++;
		if (cnt == 18) {
			GetComponent<Light> ().intensity = 0.0f;
		}
		if (cnt == 33) {
			GetComponent<Light> ().intensity = 8.0f;
		}
		if (cnt == 40) {
			GetComponent<Light> ().intensity = 0.0f;
		}
		if (cnt == 45) {
			GetComponent<Light> ().intensity = 8.0f;
		}
		if (cnt == 80) {
			GetComponent<Light> ().intensity = 0.0f;
		}
		if (cnt == 145) {
			GetComponent<Light> ().intensity = 8.0f;
			cnt = 0;
		}

	}
}
