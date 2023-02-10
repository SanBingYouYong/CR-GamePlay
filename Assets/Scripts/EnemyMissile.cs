using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public GameObject protagonist;

    public float speed = 25f;

    public WorldWill worldWill;

    public AudioSource audioSource;
    public ParticleSystem explosionParticles;

    public float lifetime = 30f;

    public float damage = 30f;

    private void Start()
    {
        protagonist = GameObject.Find("HullProtagonist");
        worldWill = GameObject.Find("WorldWill").GetComponent<WorldWill>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //rotation
        Vector3 targetPos = protagonist.transform.position;
        transform.LookAt(targetPos);
        // height restriction
        if (transform.position.y <= -200 || transform.position.y >= 50)
        {
            StartCoroutine(Explode(false));
        }
        // lifetime update
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Explode(true);
        }
    }

    private void FixedUpdate()
    {
        if (protagonist != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, protagonist.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "HullProtagonist")
        {
            StartCoroutine(Explode());
            protagonist.GetComponent<Protagonist>().health -= damage;
        }
        else if (collision.gameObject.name.Contains("shell"))
        {
            StartCoroutine(Explode());
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator Explode(bool explosion=true)
    {
        worldWill.currentMissiles.Remove(gameObject);
        Vector3 curPos = transform.position;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (explosion)
        {
            audioSource.Play();
            explosionParticles.Play();
            worldWill.GenerateWreck(curPos);
            yield return new WaitForSeconds(1);
        }
        Destroy(gameObject);
    }
}
