/**************************************************************************************************

 @File   : [ InputManager ] 
 @Auther : Unisawa

**************************************************************************************************/
using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public GameObject PlayerOne;
    public GameObject PlayerTwo;

    public float MoveSpeed;

    public float MaxHeightRange;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {

    }

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start () 
    {

    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update () 
    {
        // 1P操作
        if(Input.GetKey(KeyCode.W))
        {
            Vector3 Pos = PlayerOne.transform.position;
            Pos.y += MoveSpeed;
            Pos.y = Mathf.Clamp(Pos.y, -MaxHeightRange, MaxHeightRange);
            PlayerOne.transform.position = Pos;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 Pos = PlayerOne.transform.position;
            Pos.y -= MoveSpeed;
            Pos.y = Mathf.Clamp(Pos.y, -MaxHeightRange, MaxHeightRange);
            PlayerOne.transform.position = Pos;
        }

        // 2P操作
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 Pos = PlayerTwo.transform.position;
            Pos.y += MoveSpeed;
            Pos.y = Mathf.Clamp(Pos.y, -MaxHeightRange, MaxHeightRange);
            PlayerTwo.transform.position = Pos;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 Pos = PlayerTwo.transform.position;
            Pos.y -= MoveSpeed;
            Pos.y = Mathf.Clamp(Pos.y, -MaxHeightRange, MaxHeightRange);
            PlayerTwo.transform.position = Pos;
        }
    }
}

//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//