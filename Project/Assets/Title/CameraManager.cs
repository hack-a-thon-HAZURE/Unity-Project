﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public float posX01;
	public float posY01;
	public float posZ01;

	public float posX02;
	public float posY02;
	public float posZ02;

	public float rotX01;
	public float rotY01;
	public float rotZ01;

	public float rotX02;
	public float rotY02;
	public float rotZ02;

	private Vector3 mypos;
	private Vector4 myrot;

	private bool flg01 = false;
	private bool flg02 = false;

    private bool isStart;

    public GameObject PUshButton;

	// Use this for initialization
	void Start () {

        // エディター外ではマウスカーソルを消す
        Cursor.lockState = CursorLockMode.Locked;

#if UNITY_EDITOR
        Cursor.visible = true;
#else
        Cursor.visible = false;
#endif

		mypos.x = 12.67f;
		mypos.y = 11.41f;
		mypos.z = 19.94f;

		myrot.x = 11.7f;
		myrot.y = 232.33f;
		myrot.z = 355.59f;

        PUshButton.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () 
	{
        if ( Input.GetKey(KeyCode.Escape) )
        {
            Application.Quit();
        }

        if((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && (!isStart))
        {
            isStart = true;
            PUshButton.SetActive(false);
        }

        if(!isStart) return;

		if (flg01 == false) 
		{
			mypos.x += (posX01 - mypos.x)*0.01f;
			mypos.y += (posY01 - mypos.y)*0.01f;
			mypos.z += (posZ01 - mypos.z)*0.01f;

			myrot.x += (rotX01 - myrot.x)*0.01f;
			myrot.y += (rotY01 - myrot.y)*0.01f;
			myrot.z += (rotZ01 - myrot.z)*0.01f;
			if (mypos.x >= posX01-2.5f&&mypos.x <= posX01+2.5f) 
			{
				flg01 = true;
			}

		} 
		else if (flg02 == false) 
		{
			mypos.x += (posX02 - mypos.x)*0.02f;
			mypos.y += (posY02 - mypos.y)*0.02f;
			mypos.z += (posZ02 - mypos.z)*0.02f;

			myrot.x += (rotX02 - myrot.x)*0.03f;
			myrot.y += (rotY02 - myrot.y)*0.03f;
			myrot.z += (rotZ02 - myrot.z)*0.03f;
			if (myrot.x >= rotX02-0.5f&&myrot.x <= rotX02+0.5f) 
			{
				flg02 = true;
			}
			
		}

        if(flg02)
        {
            sceneManager.NextScene("Pong");

            AudioManager.Instance.BGMSource.volume = 0.0f;
            AudioManager.Instance.BGMStop();
        }

		GetComponent<Transform>().position = mypos; 
		GetComponent<Transform>().rotation = Quaternion.Euler(myrot.x, myrot.y, myrot.z);
	}
}
