using UnityEngine;
using Photon.Pun;
using System.Data;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;

    public float minX, minY, maxX, maxY;
    void Start()
    {
        Vector2 randomPosition = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );
        PhotonNetwork.Instantiate(
            player.name,
            randomPosition,
            Quaternion.identity 
        );
    }
}
