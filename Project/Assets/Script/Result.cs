using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Image YES;
    public Image NO;

    private bool is_continue;

    void Awake()
    {
        is_continue = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            if (is_continue) { sceneManager.NextScene("Main"); }
            else { sceneManager.NextScene("Title"); AudioManager.Instance.BGMPlay("GameCenter_Ambient"); }
        }

        if (Input.GetAxis("Mouse X") < 0.0f) is_continue = true;
        if (Input.GetAxis("Mouse X") > 0.0f) is_continue = false;

        if ( is_continue) { YES.color = new Color(1.0f, 1.0f, 1.0f, 1.0f); NO.color = new Color(1.0f, 1.0f, 1.0f, 0.5f); }
        if (!is_continue) { YES.color = new Color(1.0f, 1.0f, 1.0f, 0.5f); NO.color = new Color(1.0f, 1.0f, 1.0f, 1.0f); }
    }
}
