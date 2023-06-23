using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSoul : MonoBehaviour
{
    [SerializeField]public Transform spawnPoint;
    [SerializeField]public GameObject spawnObject;
    private GameObject SpawnGO;

    public void Spawn()
    {
            SpawnGO = Instantiate(spawnObject, spawnPoint);
    }
}
