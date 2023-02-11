using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float lifetime = 15f;

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// After hitting enemy, disable self's render and speed up lifetime. 
    /// In effect, now player is able to destroy multiple targets along 
    /// its path and in 1 sec's distance. 
    /// </summary>
    public void Vanish()
    {
        GetComponent<MeshRenderer>().enabled = false;
        lifetime = 1f;
    }
}
