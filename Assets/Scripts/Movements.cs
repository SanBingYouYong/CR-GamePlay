using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movements : MonoBehaviour
{

    public GameObject controlledGO;
    public GameObject targetTerrain;

    private float moveSpeed = 30f;
    private float rotateSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ClickMovement();
    }

    private void FixedUpdate()
    {
        var movementWS = transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Vector3 rotationAD = Vector3.up * Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.Rotate(rotationAD);
        transform.position += movementWS;
    }

    private void ClickMovement()
    {
        // obsolete click movement code: 
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (targetTerrain.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
            {
                controlledGO.transform.position = hit.point;
                controlledGO.GetComponent<NavMeshAgent>().destination = hit.point;
                controlledGO.GetComponent<NavMeshAgent>().updateRotation = false;
            }
        }
    }
}
