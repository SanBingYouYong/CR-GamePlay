using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UI;
using UnityEngine;

public class WorldWill : MonoBehaviour
{

    public int maxEnemies = 10;

    public List<GameObject> currentMissiles;

    public GameObject protagonist;

    public GameObject missile;

    public List<GameObject> missileWreckModels;
    private int wreckPoolSize;

    private int maximumWreck = 10;
    private Queue<GameObject> generatedWrecks;

    public float wreckSpeed = 10f;

    public float enemySpawnGap = 1f;
    private float spawnCountDown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //currentMissiles = new List<GameObject>();
        protagonist = GameObject.Find("HullProtagonist");
        wreckPoolSize = missileWreckModels.Count - 1;
        generatedWrecks = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (protagonist == null)
        {
            Debug.Log("Peace. Do nothing. ");
            return;
        }
        if (currentMissiles.Count < maxEnemies)
        {
            spawnCountDown -= Time.deltaTime;
            if (spawnCountDown <= 0f)
            {
                Debug.Log("Generating missile: " + currentMissiles.Count);
                spawnCountDown = enemySpawnGap;
                SpawnMissile();
            }
        }
        else
        {
            Debug.Log("Too many missiles! " + currentMissiles.Count);
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void SpawnMissile()
    {
        Vector3 centre = protagonist.transform.position;
        Vector3 missilePosition = new Vector3(
            Random.Range(centre.x - 100, centre.x + 100), 
            Random.Range(40, 50), // height
            Random.Range(centre.z - 100, centre.z + 100));
        Vector3 orientation = new Vector3(
            0f, 0f, 0f);
        currentMissiles.Add(
            Instantiate(missile, missilePosition, Quaternion.Euler(orientation)));
    }

    public GameObject GetRandomWreckModel()
    {
        return missileWreckModels[Random.Range(0, wreckPoolSize)];
    }

    public void GenerateWreck(Vector3 pos)
    {
        if (generatedWrecks.Count < maximumWreck)
        {
            Debug.Log("Generating wreck at: " + pos);
            GameObject wreck = Instantiate(GetRandomWreckModel(), pos, Quaternion.identity);
            generatedWrecks.Enqueue(wreck);
            // add random force
            wreck.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * wreckSpeed);
        }
        else
        {
            DestroySomeWrecks(); // will instantiate successfully for next wreck. 
        }
    }

    private void DestroySomeWrecks(float percentage=0.5f)
    {
        int toBeDestroyed = (int)(maximumWreck * percentage);
        for (int i = 0; i < toBeDestroyed; i++)
        {
            if (generatedWrecks.Count > 0)
            {
                Destroy(generatedWrecks.Dequeue());
            }
            else
            {
                break;
            }
        }
    }
}
