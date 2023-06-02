using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Finish : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera camera;

    private void Lock() => camera.enabled = false;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        { Lock(); }
    }
}
