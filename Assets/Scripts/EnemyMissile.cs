using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    private GameObject protagonist;
    private WorldWill worldWill;

    // MISSILE STATS: //
    /// <summary>
    /// Speed of missile movements. 
    /// </summary>
    public float speed = 25f;
    /// <summary>
    /// After specified lifetime, missile will explode. 
    /// </summary>
    public float lifetime = 30f;
    /// <summary>
    /// Damage it deals to the protagonist when hit. 
    /// </summary>
    public float damage = 30f;

    // EFFECTS: //
    public AudioSource explosionSound;
    public ParticleSystem explosionParticles;
    public ParticleSystem engineSmoke;

    private void Start()
    {
        protagonist = GameObject.Find("HullProtagonist");
        worldWill = GameObject.Find("WorldWill").GetComponent<WorldWill>();
        explosionSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Rotation
        Vector3 targetPos = protagonist.transform.position;
        transform.LookAt(targetPos);
        // Height restriction
        if (transform.position.y <= -200 || transform.position.y >= 50)
        {
            StartCoroutine(Explode(false));
        }
        // Lifetime update
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Explode(true);
        }
    }

    private void FixedUpdate()
    {
        // Move towards protagonist. 
        if (protagonist != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, protagonist.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CoroutineUtils.Sleep(0.1f);
        if (collision.gameObject.name == "HullProtagonist")
        {
            StartCoroutine(Explode());
            protagonist.GetComponent<Protagonist>().health -= damage;
        }
        else if (collision.gameObject.name.Contains("shell"))
        {
            StartCoroutine(Explode());
            collision.gameObject.GetComponent<Shell>().Vanish();
        }
    }

    /// <summary>
    /// Missile explodes: no more engine smoke, remove from missiles list, disable render
    /// and generate wreck at position. Wait until all above happened and then destroy
    /// the GameObject. 
    /// </summary>
    /// <param name="explosion">Set to false to destroy missile silently. </param>
    /// <returns></returns>
    public IEnumerator Explode(bool explosion=true)
    {
        engineSmoke.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        worldWill.currentMissiles.Remove(gameObject);
        Vector3 curPos = transform.position;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (explosion)
        {
            explosionSound.Play();
            explosionParticles.Play();
            worldWill.GenerateWreck(curPos);
            yield return new WaitForSeconds(1);
        }
        Destroy(gameObject);
    }
}
