using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance = null;

    public GameObject spawnPoint;
    public GameObject[] enemies;
    public int maxEnemiesOnScreen;
    public int totalEnemies;
    public int enemiesPerSpawn;

    int enemiesOnScreen = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);

        }

        DontDestroyOnLoad(gameObject);


    }
    void Start()
    {
        Spawn();
        
    }

    void Update()
    {
        
    }

    void Spawn()
    {
        if (enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies) 
        {
            for (int i =0; i < enemiesPerSpawn; i++)
            {
                if (enemiesPerSpawn < maxEnemiesOnScreen) 
                { 
                    GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                    enemiesOnScreen += 1;
                }

            }
        
        }
    }

    public void removeEnemyFromScreen()
    {
        if (enemiesOnScreen > 0) 
        {
            enemiesOnScreen -= 1;

        }
    }
}
