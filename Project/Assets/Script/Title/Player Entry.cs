/**************************************************************************************************

 @File   : [ Player Entry ] 
 @Auther : Ayumi Yasui

**************************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

namespace title
{

    public class PlayerEntry : MonoBehaviour
    {

        /// メンバ変数
        public GameObject EntryPrefab;    // Entryプレハブ
        public GameObject OKPrefab;       // OKプレハブ

        private bool is_ok;         // エントリー済み
        private Vector3 status_pos;    // ステータス表示位置
        private GameObject status;        // 現在のステータスオブジェクト

        /// プロパティ
        public bool IsOK { get { return is_ok; } }  // これはエントリー済みか

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            var pos = GetComponent<RectTransform>().position;

            status = GameObject.Instantiate(EntryPrefab);
            status.transform.SetParent(gameObject.transform.parent);
            status_pos = pos + new Vector3(0.0f, -100.0f, 0.0f);
            status.transform.position = status_pos;
            is_ok = false;
        }


        /// <summary>
        // Chenge Entry Is OK
        /// </summary>
        protected void ChengeOK()
        {
            if (is_ok) return;
            is_ok = true;
            Destroy(status);
            status = GameObject.Instantiate(OKPrefab);
            status.transform.SetParent(gameObject.transform.parent);
            status.transform.position = status_pos;
        }
    }

}   // namespace title
//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//