using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainGun : MonoBehaviour
{
    private GameObject protagonist;
    
    // SHELL: //
    /// <summary>
    /// Prefab of shell. 
    /// </summary>
    public GameObject shell;
    /// <summary>
    /// Shell's fly speed. 
    /// </summary>
    public float shellSpeed = 2500f;

    // FIRE: //
    /// <summary>
    /// Gun's fire rate. 
    /// </summary>
    public float fireSpeed = 0.1f;
    private float fireCounter = 0f;
    public bool firing;

    // EFFECTS: //
    public AudioSource fireSound;
    public ParticleSystem explosionParticles;

    // CAMERA: //
    public CinemachineVirtualCamera vcam;
    /// <summary>
    /// Rerefence to camera's noise model; for enabling/disabling screenshakes. 
    /// </summary>
    public CinemachineBasicMultiChannelPerlin noise;

    // Start is called before the first frame update
    void Start()
    {
        fireSound = GetComponent<AudioSource>();
        protagonist = GameObject.Find("HullProtagonist");
        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        // disable firing after protagonist died. 
        if (!protagonist.GetComponent<Protagonist>().alive)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            fireCounter -= Time.deltaTime;
            firing = true;
            if (fireCounter < 0f)
            {
                Fire();
                fireCounter = fireSpeed;
            }
            // enable screenshake: receiving full effect from noise model. 
            noise.m_AmplitudeGain = 1;
        }
        else
        {
            firing = false;
            // disable screenshake. 
            noise.m_AmplitudeGain = 0;
        }
    }

    /// <summary>
    /// Main gun opens fire. Play effects, instantiates a new shell and launch it
    /// with random rotation applied - to have "less accuracy". 
    /// </summary>
    private void Fire()
    {
        explosionParticles.Play();
        fireSound.Play();
        // Shell with random rotation: 
        Vector3 shellRotation = transform.rotation.eulerAngles;
        shellRotation.x += Random.Range(-10f, 10f);
        shellRotation.y += Random.Range(-10f, 10f);
        shellRotation.z += Random.Range(-10f, 10f);
        GameObject generatedShell = Instantiate(
            shell, transform.position, Quaternion.Euler(shellRotation));
        // Launch shell forward: 
        generatedShell.GetComponent<Rigidbody>().AddRelativeForce(
            new Vector3(0, 0, shellSpeed));
    }
}
