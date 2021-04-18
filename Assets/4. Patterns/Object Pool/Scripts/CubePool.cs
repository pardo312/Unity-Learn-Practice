using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] GameObject cubePrefab;
    [SerializeField] int size;
    [SerializeField] bool needForPreserveCubes;
    [SerializeField] bool shouldExpand;
    public List<GameObject> pool;

    private void Awake() {
        pool = new List<GameObject>();

        for(int i = 0; i<size;i++){
            GameObject cube = Instantiate(cubePrefab,this.transform);
            cube.SetActive(false);
            pool.Add(cube);
        }
    }
    public GameObject GetCubeFromPool(){

        if (needForPreserveCubes)
        {
            GameObject newCube = pool[0];
            pool.Remove(newCube);
            setCube(newCube);
            pool.Add(newCube);
            return newCube;
        }
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                setCube(pool[i]);
                return pool[i];
            }
        }
        if (shouldExpand)
        {
            GameObject newCube = Instantiate(cubePrefab,this.transform);
            pool.Add(newCube);
            setCube(newCube);
            return newCube;
        }
        else
            return null;
    }

    private void setCube(GameObject cube){
        cube.SetActive(true);
        cube.transform.position = Vector3.zero;
        cube.transform.position += new Vector3(Random.Range(-20,20),0,Random.Range(-20,20));
        cube.transform.localScale = new Vector3(1,1,1);
    }

}
