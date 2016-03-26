// ---------------------------------------------------------
// [ AutoDestroyParticle.cs ]
//
// [ Description: ]  自動でパーティクル終了時に自身を削除する関数
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;

public class AutoDestroyParticle : MonoBehaviour
{
    // Member
    private ParticleSystem m_ParticleSystem;
    private bool DoubleCheck;

    /// <summary>
    /// Init
    /// </summary>
    void Awake()
    {
        m_ParticleSystem = this.gameObject.GetComponent<ParticleSystem>();

        DoubleCheck = false;
    }

    /// <summary>
    /// Before Update
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Update
    /// </summary>
    void Update()
    {
        if (m_ParticleSystem != null && m_ParticleSystem.particleCount == 0)
        {
            if (DoubleCheck)
            {
                //Debug.Log("Delete");
                Destroy(this.gameObject);
            }

            DoubleCheck = true;
        }
    }
}

//=====================================================================================//
//                                                                                     //
//                                     @End of File                                    //
//                                                                                     //
//=====================================================================================//