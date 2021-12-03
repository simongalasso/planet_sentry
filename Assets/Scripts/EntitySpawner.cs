using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntitySpawner : MonoBehaviour
{
    public GameObject nbWaveTextObject;
    public GameObject Player;
    public GameObject Entity;
    public GameObject Boss;
    public float nbWaves;
    public float nbEnemiesPerWave;
    public float delayBetweenWave;
    public float delayBetweenEnemies;
    public bool endBoss;

    private float numEnemy = 0;
    private float numWave = 0;
    private float timeEnemy = 0;
    private float timeWave = 0;

    private bool enemyOnZone = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (numWave < nbWaves)
        {
            if (numEnemy < nbEnemiesPerWave)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.8f);
                if (hitColliders.Length == 1 && timeEnemy >= delayBetweenEnemies)
                {
                    float newY = transform.rotation.y + (Random.Range(-90, 90));
                    transform.Rotate(0, newY, 0);
                    Instantiate(Entity.transform, transform.position, transform.rotation);
                    numEnemy++;
                    timeEnemy = 0;
                }
                timeEnemy += 1 * Time.deltaTime;
            }
            else if (timeWave >= delayBetweenWave)
            {
                numEnemy = 0;
                timeEnemy = 0;
                timeWave = 0;
                numWave++;
            }
            if ((GameObject.FindGameObjectsWithTag("Enemy")).Length == 0)
                timeWave += 1 * Time.deltaTime;
            nbWaveTextObject.GetComponent<Text>().text = "Wave " + (numWave + 1);
        }
        else if (endBoss)
        {
            nbWaveTextObject.GetComponent<Text>().text = "Wave Boss";
            float newY = transform.rotation.y + (Random.Range(-90, 90));
            transform.Rotate(0, newY, 0);
            Instantiate(Boss.transform, transform.position, transform.rotation);
            endBoss = false;
        }
    }
}
