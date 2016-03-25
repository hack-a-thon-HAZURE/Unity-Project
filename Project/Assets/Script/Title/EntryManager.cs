/**************************************************************************************************

 @File   : [ EntryManager ] 
 @Auther : Ayumi Yasui

**************************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

namespace title
{

    public class EntryManager : MonoBehaviour
    {
        // メンバ変数
        public PlayerEntry[] Entrys;

        /// <summary>
        // Update is called once per frame
        /// </summary>
        void Update()
        {
            // すべてエントリーしているのか？
            for (int i = 0; i < Entrys.Length; ++i)
            {
                if (!Entrys[i].IsOK) return;
            }

            // 以下、すべてエントリー済み
            // 画面遷移
            sceneManager.NextScene("Main");
        }
    }
}   // namespace title
//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//