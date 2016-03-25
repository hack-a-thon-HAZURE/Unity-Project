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
    public float MaxMoveSpeed;

    public float MaxHeightRange;


    private Rigidbody PlayerOneRigid;
    private Rigidbody PlayerTwoRigid;

    private float PlayerOneVirtualKey;
    private float PlayerTwoVirtualKey;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        PlayerOneVirtualKey = 0;
        PlayerTwoVirtualKey = 0;
    }

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start () 
    {
        PlayerOneRigid = PlayerOne.GetComponent<Rigidbody>();
        PlayerTwoRigid = PlayerTwo.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update () 
    {
        // 1P操作
        {
            Vector3 Pos = PlayerOne.transform.position;
            if (Input.GetKey(KeyCode.W))
            {
                PlayerOneVirtualKey += MoveSpeed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                PlayerOneVirtualKey -= MoveSpeed;
            }

            // Mouse Xにアサイン
            {
                float move_1p = Input.GetAxis("Mouse X");
                PlayerOneVirtualKey -= move_1p;
            }

            PlayerOneVirtualKey += (0 - PlayerOneVirtualKey) * 0.5f;
            float add = PlayerOneVirtualKey;

            if (add < -MaxMoveSpeed) add = -MaxMoveSpeed;
            if (add >  MaxMoveSpeed) add =  MaxMoveSpeed;

            Pos.y += add;
            Pos.y = Mathf.Clamp(Pos.y, -MaxHeightRange, MaxHeightRange);
            PlayerOneRigid.MovePosition(Pos);
        }

        // 1P操作
        {
            Vector3 Pos = PlayerTwo.transform.position;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                PlayerTwoVirtualKey += MoveSpeed;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                PlayerTwoVirtualKey -= MoveSpeed;
            }

            {
                float move_1p = Input.GetAxis("Mouse Y");
                PlayerTwoVirtualKey -= move_1p;
            }

            PlayerTwoVirtualKey += (0 - PlayerTwoVirtualKey) * 0.5f;
            float add = PlayerTwoVirtualKey;

            if (add < -MaxMoveSpeed) add = -MaxMoveSpeed;
            if (add > MaxMoveSpeed) add = MaxMoveSpeed;

            Pos.y += add;
            Pos.y = Mathf.Clamp(Pos.y, -MaxHeightRange, MaxHeightRange);
            PlayerTwoRigid.MovePosition(Pos);
        }

    }
}

//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//