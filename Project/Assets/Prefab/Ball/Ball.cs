/**************************************************************************************************

 @File   : [ Ball ] 
 @Auther : Unisawa

**************************************************************************************************/
using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    // メンバ変数
    public float InitBallSpeed; // ボールの初速度
    public float AddBallSpeed;  // ボールの加速度
    public string[] HitIgnoreList;  // 反射処理無視リスト
    public float StartMotionTime;   // スタート時のモーション時間

    private Rigidbody rigid;
    private Vector2 vec;    // ベクトル
    private int layerMask;  // レイヤーマスク
    private LineRenderer line_renderer;

    // プロパティ
    public Vector2 Vec { get { return vec; } set { vec = value; } }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        vec = new Vector2();
        rigid = GetComponent<Rigidbody>();

        // ボールが飛ぶ方向(左右)をランダムで決めている
        float LR = (Random.Range(0, 2) == 0 ? -1 : 1);
        float RotZ = Random.Range(Mathf.PI / 4.0f, -Mathf.PI / 4.0f);

        // ベクトルの生成
        var DirNor = transform.right;
        DirNor *= LR;

        vec.x = Mathf.Cos(RotZ) * DirNor.x + Mathf.Sin(RotZ)  * DirNor.y;
        vec.y = -Mathf.Sin(RotZ) * DirNor.x + Mathf.Cos(RotZ) * DirNor.y;
        vec *= InitBallSpeed;

        // レイヤーマスク作成
        int ignore = LayerMask.GetMask(HitIgnoreList); ;
        layerMask = Physics.DefaultRaycastLayers ^ ignore;

        // ラインレンダラーの作成
        line_renderer = gameObject.AddComponent<LineRenderer>();
        line_renderer.SetVertexCount(2);
        line_renderer.material = new Material(Shader.Find("Particles/Additive"));
        Vector3[] positions = { Vector3.zero, vec };
        line_renderer.SetPositions(positions);
        line_renderer.SetColors(Color.white, new Color(1.0f, 1.0f, 1.0f, 0.0f));
        line_renderer.SetWidth(0.05f, 0.1f);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        // カットイン中は動きを止める
        if (GameRuleManager.IsCutIn) return;

        if (StartMotionTime > 0.0f)
        {
            StartMotionTime -= Time.deltaTime;
            if (StartMotionTime <= 0.0f) Destroy(line_renderer);
            return;
        }

        // あたり判定の結果によって、ボールの動きが変化する
        var info = new RaycastHit();
        bool is_will_hit =
            Physics.Raycast(transform.position, vec.normalized, out info, vec.magnitude * Time.deltaTime , layerMask);
        if (!is_will_hit) { Move(); }
        else { Move(info); }
        Debug.DrawRay(transform.position, vec*Time.deltaTime, Color.red);
    }

    /// <summary>
    /// ボールの移動(あたり判定から補間)
    /// </summary>
    private void Move(RaycastHit info)
    {
        //Debug.Log("Hit Ball : " + info.collider.name);
        Vector2 pos_old = transform.position;
        Vector2 info_point = info.point;

        // 現在のベクトルの分解
        var vec_nor    = vec.normalized;
        var vec_length = vec.magnitude;

        // 当たる位置までの距離
        var move_before_hit = info_point - pos_old;
        var length_before_hit = move_before_hit.magnitude;

        // 当たった後の残り移動距離
        var length_after_hit = vec_length - length_before_hit;

        // ベクトルの反転
        var vec_ref_nor = Vector2.Reflect(vec_nor, info.normal);

        // 移動結果後の位置
        transform.position = pos_old + ((vec_nor * length_before_hit) + (vec_ref_nor * length_after_hit)) * Time.deltaTime;
        rigid.MovePosition(transform.position);

        // 最終的なベクトル
        vec = vec_ref_nor * vec_length;

        // 加速
        AddSpeedIfHit();
        CollisionEnter(info.collider);
    }

    /// <summary>
    /// ボールの移動
    /// </summary>
    private void Move()
    {
        // ベクトルを足す
        Vector2 pos = transform.position;
        pos += vec * Time.deltaTime;
        transform.position = pos;
    }



    /// <summary>
    /// 何かに当たったら加速
    /// </summary>
    private void AddSpeedIfHit()
    {
        var vec_nor = vec.normalized;
        var speed = vec.magnitude;
        vec = vec_nor * (speed + AddBallSpeed);
    }
    /// <summary>
    /// 弾と何かが当たったときの
    /// </summary>
    /// <param name="Col"></param>
    void CollisionEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Goal")
        {
            //Col.gameObject.GetComponent<Goal>().TriggerEnter(Col);
            //Destroy(this.gameObject);
        }

        if (Col.gameObject.tag == "Wall")
        {
            Col.gameObject.GetComponent<AudioPlayer>().Play();
        }

        if (Col.gameObject.tag == "Shield")
        {
            Col.gameObject.GetComponent<Shield>().TriggerEnter(Col);
        }

        if (Col.gameObject.tag == "Player")
        {
            Col.gameObject.GetComponent<AudioPlayer>().Play();
            Col.gameObject.GetComponent<Player>().TriggerEnter(Col);
        }
    }


}
//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//