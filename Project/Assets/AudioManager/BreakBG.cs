using UnityEngine;
using System.Collections;

public class BreakBG : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().mass = Random.Range(0.5f, 30.0f);

        GetComponent<Rigidbody>().useGravity = true;

        GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(50.0f, 100.0f), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}