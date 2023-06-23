using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject spawnObject;

    private GameObject runtimeSpawnGO;

    private void Update()
    {
        Spawn();
    }


    public void Spawn()
    {
        runtimeSpawnGO = Instantiate(spawnObject, spawnPoint);
    }

}
