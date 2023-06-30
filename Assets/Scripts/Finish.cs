using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Finish : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("BBBB");
            camera.Follow = null;
        }
    }
}
