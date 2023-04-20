using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    //public float timeBetweenSpawn = -0.2f;
    //public float startTimeBetweenSpawn = 0.0f;

    public GameObject echo;
    public bool canGenerate = false;

    // Update is called once per frame
    void Update()
    {
        if (canGenerate)
        {
            GameObject instance = (GameObject)Instantiate(echo, transform.position, Quaternion.identity);
            Destroy(instance, 0.2f);
            //timeBetweenSpawn = startTimeBetweenSpawn;
        }

    }
}
