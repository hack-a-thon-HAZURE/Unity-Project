/**************************************************************************************************

 @File   : [ Player2Entry ] 
 @Auther : Ayumi Yasui

**************************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

namespace title
{

    public class Player2Entry : PlayerEntry
    {
        /// <summary>
        // Update is called once per frame
        /// </summary>
        void Update()
        {
            if (Input.GetMouseButtonDown(1)) ChengeOK();
        }
    }
}   // namespace title
//===============================================================================================//
//                                                                                               //
//                                          @End of File                                         //
//                                                                                               //
//===============================================================================================//