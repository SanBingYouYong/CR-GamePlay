using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldWill : MonoBehaviour
{

    public int maxEnemies = 10;

    public int currentMissiles = 0;

    public GameObject protagonist;

    public GameObject missile;

    public List<GameObject> missileWreckModels;
    private int wreckPoolSize;

    private int maximumWreck = 10;
    private Queue<GameObject> generatedWrecks;

    public float wreckSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
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
        if (currentMissiles <= maxEnemies)
        {
            SpawnMissile();
        }
    }

    private void SpawnMissile()
    {
        Vector3 centre = protagonist.transform.position;
        Vector3 missilePosition = new Vector3(
            Random.Range(centre.x - 100, centre.x + 100), 
            Random.Range(0, 10), // height
            Random.Range(centre.z - 100, centre.z + 100));
        Vector3 orientation = new Vector3(
            0f, 0f, 0f);
        Instantiate(missile, missilePosition, Quaternion.Euler(orientation));
        currentMissiles++;
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
