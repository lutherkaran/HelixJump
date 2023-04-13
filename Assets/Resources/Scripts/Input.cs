using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input : MonoBehaviour
{
    private readonly float angle = 1.5f;
    public bool bInput = true;

    private void OnEnable()
    {
        Ball.ballDied += PlayerInput;
    }
    void PlayerInput(bool bDied)
    {
        //Debug.Log("Player input is blocked");
        bInput = bDied;
    }
    private void OnMouseDrag()
    {
        if (bInput)
        {
            float x = UnityEngine.Input.GetAxis("Mouse X");
            this.transform.GetChild(0).transform.RotateAround(this.transform.position, new Vector3(0f, 1f, 0f) * Time.deltaTime * -1f * x, angle);
        }
    }
    private void OnDisable()
    {
        Ball.ballDied -= PlayerInput;
    }

}
