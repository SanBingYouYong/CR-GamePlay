using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movements : MonoBehaviour
{
    private MainGun mainGun;

    public float moveSpeed = 30f;
    public float moveSpeedFiring = 20f;
    public float rotateSpeed = 100f;
    public float rotateSpeedFiring = 20f;

    // Start is called before the first frame update
    void Start()
    {
        mainGun = GameObject.Find("MainGun.001").GetComponent<MainGun>();
    }

    private void FixedUpdate()
    {
        // Disable movements after being dead. 
        if (!GetComponent<Protagonist>().alive)
        {
            return;
        }
        // Determine movements: firing slows down vehicle. 
        Vector3 movementWS;
        Vector3 rotationAD;
        if (mainGun.firing)
        {
            movementWS = transform.forward * Input.GetAxis("Vertical") * moveSpeedFiring * Time.deltaTime;
            rotationAD = Vector3.up * Input.GetAxis("Horizontal") * rotateSpeedFiring * Time.deltaTime;
        }
        else
        {
            movementWS = transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            rotationAD = Vector3.up * Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        }
        transform.Rotate(rotationAD);
        transform.position += movementWS;
    }
}
