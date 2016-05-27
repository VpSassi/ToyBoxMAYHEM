using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public PlayerMovement pM;
    public GameObject zombear;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    void Start()
    {
    
        InvokeRepeating("Spawn", spawnTime, spawnTime);

       
    }



    void Spawn()
    {
        if(pM.playerHP <= 0)
        {
            return;

        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(zombear, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
