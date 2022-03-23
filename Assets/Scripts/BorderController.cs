using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    public Transform topBorder;
    public Transform bottomBorder;
    public float moveSpeed;
    private float bottomBorderMoveSpeed;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameManager.GameEvents.onTopWallShrink += Movedown;
        GameManager.GameEvents.onBottomWallShrink += Moveup;
    }

    void OnDisable()
    {
        GameManager.GameEvents.onTopWallShrink -= Movedown;
        GameManager.GameEvents.onBottomWallShrink -= Moveup;
    }

    // Update is called once per frame
    void Movedown()
    {
        topBorder.position += Vector3.down * moveSpeed;
    }
    void Moveup()
    {
        bottomBorder.position += Vector3.up * bottomBorderMoveSpeed;
    }
}