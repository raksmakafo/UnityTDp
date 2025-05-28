using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 


public class EnemySpawnerScr : MonoBehaviour
{

    public float SpawnTime = 4f; 
    int SpawnCount = 0;

    public GameObject EnemyPref, WayPointParent;

    void Update()
    {
        if (SpawnTime <= 0)
        {
            StartCoroutine(SpawnEnemy(1));
            SpawnTime = 4f; // Reset the spawn time to 2 seconds after spawning an enemy
        }
        SpawnTime -= Time.deltaTime; // Decrease the spawn time by the time passed since last frame

        GameObject.Find("SpawnCountTxt").GetComponent<TMP_Text>().text = Mathf.Round(SpawnTime).ToString(); // Update the spawn count text in the UI
    }

    IEnumerator SpawnEnemy(int EnemyCount)
    {
        SpawnCount++;

        for (int i = 0; i < EnemyCount; i++)
        {
            GameObject tmpEnemy = Instantiate(EnemyPref);
            tmpEnemy.transform.SetParent(gameObject.transform, false);

            tmpEnemy.tag = "Enemy";

            tmpEnemy.GetComponent<EnemyScr>().wayPointParent = WayPointParent; // Set the wayPointParent for the enemy
            yield return new WaitForSeconds(0.3f);// Wait for a short time before spawning enemies

        }
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScr : MonoBehaviour
{
    public float SpawnDelay = 2f;
    private float timer = 0f;

    public GameObject EnemyPref, WayPointParent;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= SpawnDelay)
        {
            SpawnEnemy();
            timer = 0f; // סבנמס עאילונא
        }
    }

    void SpawnEnemy()
    {
        GameObject tmpEnemy = Instantiate(EnemyPref, transform.position, Quaternion.identity);
        tmpEnemy.transform.SetParent(transform, false);
        tmpEnemy.GetComponent<EnemyScr>().wayPointParent = WayPointParent;
    }
}

*/