using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Finish : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    public void Cam(bool active) => camera.enabled = active;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        { 
            Cam(false);
        }
    }
}
