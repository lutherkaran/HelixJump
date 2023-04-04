using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public float angle;
    private void OnMouseDrag()
    {
        if (Ball.Instance.bPlayer == true)
        {
            float x = Input.GetAxis("Mouse X");
            transform.GetChild(0).transform.RotateAround(transform.position, new Vector3(0f, 1f, 0f) * Time.deltaTime * -1f * x, angle);
        }
    }

}
