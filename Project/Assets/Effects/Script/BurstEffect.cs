using UnityEngine;
using System.Collections;

public class BurstEffect : MonoBehaviour {

    public float LifeTime = 1.0f;
    private float LifeTimeCnt =0.0f;
    public float ScalingValue = 20.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 Scl = transform.localScale;

        Scl.x += ScalingValue * Time.deltaTime;
        Scl.y += ScalingValue * Time.deltaTime;

        transform.localScale = Scl;

        LifeTimeCnt += Time.deltaTime;

        if (LifeTimeCnt >= LifeTime)
        {
            Destroy(gameObject);
        }


	}
}
