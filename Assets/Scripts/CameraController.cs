using UnityEngine;

public class CameraController : MonoBehaviour
{
    void OnPreCull()
    {
        GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);

        var aspect = (float)Screen.width / (float)Screen.height;
        var scale = Mathf.Max(1.0f / aspect, 1.0f);

        GetComponent<Camera>().rect = new Rect(0, 0, scale, scale);
    }
}
