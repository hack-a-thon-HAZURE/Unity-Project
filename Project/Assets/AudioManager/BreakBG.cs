using UnityEngine;
using System.Collections;

public class BreakBG : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().mass = Random.Range(1.0f, 30.0f);

        GetComponent<Rigidbody>().useGravity = true;

        GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(50.0f, 100.0f), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.position.y <= 200.0f)
        {
            Destroy(this.gameObject);
        }
    }
}