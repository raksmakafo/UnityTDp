using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using NUnit.Framework;
using UnityEngine;

public class EnemyScr : MonoBehaviour
{
 
    List<GameObject> wayPoints = new List<GameObject>();

    public GameObject wayPointParent;

    int wayIndex = 0;
    private int speed = 3;
    public int health = 30;

    private void Start()
    {
        // wayPoints = GameObject.Find("Camera").GetComponent<GameControllerScript>().wayPoints;
        GetWayPoints();
        
    }


    void Update() {

        Move();
        CheckIsLive();

    }

    void GetWayPoints()
    {
        for (int i = 0; i < wayPointParent.transform.childCount; i++)
        {
            wayPoints.Add(wayPointParent.transform.GetChild(i).gameObject);
        }
    }

    private void Move()
    {
        Vector3 dir = wayPoints[wayIndex].transform.position - transform.position;

        transform.Translate(dir.normalized * Time.deltaTime * speed);

        if(Vector3.Distance(transform.position, wayPoints[wayIndex].transform.position)< 0.3f) {
            if (wayIndex < wayPoints.Count - 1)
                wayIndex++;
            else
                Destroy(gameObject);
        
       
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

   public void CheckIsLive()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
