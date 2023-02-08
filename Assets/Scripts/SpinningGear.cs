using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningGear : MonoBehaviour
{
    public float spinningSpeed = 360f;

    public bool forwarding = false;
    public bool reversing = false;

    private void FixedUpdate()
    {
        if (forwarding)
        {
            transform.Rotate(spinningSpeed * Time.deltaTime, 0, 0);
        }
        else if (reversing)
        {
            transform.Rotate(-spinningSpeed * Time.deltaTime, 0, 0);
        }
    }
}
