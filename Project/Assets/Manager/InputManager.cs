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

    public float HoldTime;

    private Rigidbody PlayerOneRigid;
    private Rigidbody PlayerTwoRigid;

    private float PlayerOneVirtualKey;
    private float PlayerTwoVirtualKey;

    private ForceBallSpeed PlayerOneForceBall;
    private ForceBallSpeed PlayerTwoForceBall;

    private float hold_time_1;
    private float hold_time_2;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        PlayerOneVirtualKey = 0;
        PlayerTwoVirtualKey = 0;

        hold_time_1 = 0;
        hold_time_2 = 0;

        // エディター外ではマウスカーソルを消す
#if UNITY_EDITOR
        Cursor.visible = true;
#else
        Cursor.visible = false;
#endif
    }

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start()
    {
        PlayerOneRigid = PlayerOne.GetComponent<Rigidbody>();
        PlayerTwoRigid = PlayerTwo.GetComponent<Rigidbody>();
        PlayerOneForceBall = PlayerOne.GetComponentInChildren<ForceBallSpeed>();
        PlayerTwoForceBall = PlayerTwo.GetComponentInChildren<ForceBallSpeed>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update () 
    {
        if ( Input.GetKey(KeyCode.Escape) )
        {
            Application.Quit();
        }

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
            if (add > MaxMoveSpeed) add = MaxMoveSpeed;

            Pos.y += add;
            Pos.y = Mathf.Clamp(Pos.y, -MaxHeightRange, MaxHeightRange);
            PlayerOneRigid.MovePosition(Pos);

            // キャッチしてみる
            if (Input.GetMouseButtonDown(0)) hold_time_1 = HoldTime;
            if(hold_time_1 > 0) PlayerOneForceBall.StartCatch();
            hold_time_1 -= Time.deltaTime;
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

            // キャッチしてみる
            if (Input.GetMouseButton(1)) hold_time_2 = HoldTime;
            if (hold_time_2 > 0) PlayerTwoForceBall.StartCatch();
            hold_time_2 -= Time.deltaTime;
        }

    }
}

//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
