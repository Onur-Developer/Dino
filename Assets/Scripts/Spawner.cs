using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] cactus;
    void Start()
    {
        StartCoroutine(SpawnCactus(2));
    }

    IEnumerator SpawnCactus(float time)
    {
        yield return new WaitForSecondsRealtime(Random.Range(0.1f,2));
        Instantiate(cactus[Random.Range(0,cactus.Length)], transform.position, Quaternion.identity);
        StartCoroutine(SpawnCactus(0));
    }
}
