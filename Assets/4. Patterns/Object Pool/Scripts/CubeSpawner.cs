using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{

    [SerializeField] private CubePool cubePool;
    [SerializeField] private float timeBetweenSpawns;
    private float currentTime;

    private void Start() {
        currentTime = timeBetweenSpawns;
    }

    void Update()
    {
        if(currentTime >= timeBetweenSpawns){
            GameObject cubeGO = cubePool.GetCubeFromPool();
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
    }
}
