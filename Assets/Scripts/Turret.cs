using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    private Protagonist protagonist;

    // Start is called before the first frame update
    void Start()
    {
        protagonist = GameObject.Find("HullProtagonist").GetComponent<Protagonist>();
    }

    // Update is called once per frame
    void Update()
    {
        // Disable turret rotation after death. 
        if (!protagonist.alive)
        {
            return;
        }
        // Rotation: Look at mouse direction. 
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = 90 - Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }
}
