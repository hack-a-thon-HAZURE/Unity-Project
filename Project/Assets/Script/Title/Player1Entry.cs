/**************************************************************************************************

 @File   : [ Player Entry ] 
 @Auther : Ayumi Yasui

**************************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

namespace title {

    public class Player1Entry : PlayerEntry
    {
        /// <summary>
        // Update is called once per frame
        /// </summary>
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) ChengeOK();
        }
    }
}   // namespace title
//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//