using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGun : MonoBehaviour
{

    public ParticleSystem explosionParticles;
    public GameObject shell;
    public float shellSpeed = 2500f;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        Debug.Log("Playing Firing Particles: " + explosionParticles.name);
        explosionParticles.Play();
        audioSource.Play();
        GameObject ball = Instantiate(shell, transform.position, transform.rotation);

        ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3
                                             (0, 0, shellSpeed));
    }
}
