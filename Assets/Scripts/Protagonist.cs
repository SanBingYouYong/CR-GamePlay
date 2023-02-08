using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script to be mounted on the main player pawn in the game scene. 
/// 
/// Takes care of a little bit of everything, to get things done faster. 
/// 
/// Including: navigation & moving, shooting & game play, and effects & animation. 
/// </summary>
public class Protagonist : MonoBehaviour
{
    /// <summary>
    /// To be rotated following the direction of the mouse. 
    /// </summary>
    public GameObject turret;

    
    /// <summary>
    /// To be referred to when we are to add gun fire effects . 
    /// </summary>
    public GameObject cannon;

    /// <summary>
    /// To simply make them spining as long as movement occurs. 
    /// </summary>
    public List<GameObject> gears;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            // Moving forward
            SetWheelsState(true, false);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            // Moving backward
            SetWheelsState(false, true);
        }
        else
        {
            // Not moving
            SetWheelsState(false, false);
        }
    }

    /// <summary>
    /// Set the wheels as moving forward, backward or not moving. One more signal slot (2^2 = 4) can be potentially extended to cope with turning. 
    /// -> Forward: forwarding = true, reversing = false; 
    /// -> Backward: forwarding = false, reversing = true; 
    /// -> Stopped: both = true; 
    /// </summary>
    /// <param name="forwarding"></param>
    /// <param name="reversing"></param>
    private void SetWheelsState(bool forwarding, bool reversing)
    {
        foreach (GameObject gear in gears)
        {
            gear.GetComponent<SpinningGear>().forwarding = forwarding;
            gear.GetComponent<SpinningGear>().reversing = reversing;
        }
    }
}
