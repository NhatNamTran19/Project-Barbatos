using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour
{
    public GameObject player;
    public CinemachineVirtualCamera camera;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            camera.Follow = player.transform;
        }
    }
}
