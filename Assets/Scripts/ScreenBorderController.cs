using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorderController : MonoBehaviour
{
    float screenWidth;
    // Start is called before the first frame update
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        screenWidth = Camera.main.orthographicSize * 2 * screenRatio - .1f;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        float _widthdiff = screenWidth * -Mathf.Sign(other.transform.position.x);
        other.transform.position += Vector3.right * _widthdiff;
        Debug.Log("fuck");
    }
}
