using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldWill : MonoBehaviour
{
    /// <summary>
    /// The main Player character in the game. 
    /// </summary>
    private GameObject protagonist;

    // ENEMY SPAWNING: //
    /// <summary>
    /// Maximum number of enemy (missiles) that can be in the scene. 
    /// </summary>
    public int maxEnemies = 10;
    /// <summary>
    /// The time gap between generating enemies. To avoid generating too much at the same time, to balance the game. 
    /// </summary>
    public float enemySpawnGap = 0.5f;
    /// <summary>
    /// A counter to spawn enemies. 
    /// </summary>
    private float spawnCountDown = 0f;
    /// <summary>
    /// The prefab of enemy missile. 
    /// </summary>
    public GameObject missile;
    /// <summary>
    /// A list keeping track of spawned enemies. 
    /// </summary>
    public List<GameObject> currentMissiles;

    // ENEMY WRECKAGE GENERATING: //
    /// <summary>
    /// A list containing choices of different wreck models. 
    /// </summary>
    public List<GameObject> missileWreckModels;
    /// <summary>
    /// An end index to choose different wreck models. 
    /// </summary>
    private int wreckPoolSize;
    /// <summary>
    /// Limit on number of wrecks in the scene. 
    /// </summary>
    public int maximumWreck = 100;
    /// <summary>
    /// A FIFO queue to keep track of and clean wreckages. 
    /// </summary>
    private Queue<GameObject> generatedWrecks;
    /// <summary>
    /// The initial speed wrecks fly out after instantiation. 
    /// </summary>
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
        if (currentMissiles.Count < maxEnemies)
        {
            spawnCountDown -= Time.deltaTime;
            if (spawnCountDown <= 0f)
            {
                Debug.Log("Generating missile when there are: " + currentMissiles.Count + " missiles. ");
                spawnCountDown = enemySpawnGap;
                SpawnMissile();
            }
        }
    }

    /// <summary>
    /// Instantiates enemyMissile prefab around protagonist's position in the sky. 
    /// </summary>
    private void SpawnMissile()
    {
        Vector3 centre = protagonist.transform.position;
        Vector3 missilePosition = new Vector3(
            Random.Range(centre.x - 100, centre.x + 100), 
            Random.Range(40, 50), // height, to generate above protagonist. 
            Random.Range(centre.z - 100, centre.z + 100));
        Vector3 orientation = new Vector3(0f, 0f, 0f);
        currentMissiles.Add(
            Instantiate(missile, missilePosition, Quaternion.Euler(orientation)));
    }

    /// <summary>
    /// Randomly choose a missile wreckage model to be spawend from the models pool. 
    /// </summary>
    /// <returns></returns>
    public GameObject GetRandomWreckModel()
    {
        return missileWreckModels[Random.Range(0, wreckPoolSize)];
    }

    /// <summary>
    /// Instantiate wreck model around specified position, usually where a missile exploded. 
    /// </summary>
    /// <param name="pos">The world position to spawn the wreck model at. </param>
    public void GenerateWreck(Vector3 pos)
    {
        if (generatedWrecks.Count < maximumWreck)
        {
            Debug.Log("Generating wreck at: " + pos);
            GameObject wreck = Instantiate(GetRandomWreckModel(), pos, Quaternion.identity);
            generatedWrecks.Enqueue(wreck);
            // add random force to make them fly out randomly: 
            wreck.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * wreckSpeed);
        }
        else
        {
            DestroySomeWrecks(); // will then instantiate successfully for next wreck. 
        }
    }

    /// <summary>
    /// Clean up the queue so that new wreck models can be generated. 
    /// </summary>
    /// <param name="percentage">The portion of the queue to be cleaned up. Default a half. </param>
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
