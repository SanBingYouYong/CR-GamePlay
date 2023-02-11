using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// A script to be mounted on the main player character in the game scene. 
/// </summary>
public class Protagonist : MonoBehaviour
{
    public bool alive = true;

    /// <summary>
    /// To make them spining as long as movement occurs. 
    /// </summary>
    public List<GameObject> gears;  // if have more time, this can be moved into SpinningGears.cs
    
    // HP: //
    /// <summary>
    /// Current health. 
    /// </summary>
    public float health = 100f;
    public float maxHealth = 100f;
    public float recoverSpeed = 10f;

    /// <summary>
    /// The banner that appears after protagonist dies. 
    /// </summary>
    public GameObject banner; 


    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            alive = false;
            Explode();
            return;
        }
        if (health < maxHealth)
        {
            health += recoverSpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// Takes care of gear rotations. 
    /// </summary>
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
    /// Shows the end-game banner, count own to go back to the starting scene. 
    /// </summary>
    public void Explode()
    {
        StartCoroutine(FadeIn());
        StartCoroutine(Restart());
    }

    /// <summary>
    /// Make banner image fade in. 
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // Set color with i as alpha
            banner.GetComponent<Image>().color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    /// <summary>
    /// Slows down time, stops music, wait for seconds and then
    /// change time scale back to normal and go back to starting scene. 
    /// </summary>
    /// <returns></returns>
    IEnumerator Restart()
    {
        Time.timeScale = 0.25f;
        Destroy(GameObject.Find("music"));
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// Set the wheels as moving forward, backward or not moving. One more signal slot (2^2 = 4) can be potentially extended to cope with turning. 
    /// -> Forward: forwarding = true, reversing = false; 
    /// -> Backward: forwarding = false, reversing = true; 
    /// -> Stopped: both = false; 
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
