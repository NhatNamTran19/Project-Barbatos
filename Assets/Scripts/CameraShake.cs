using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //public static CameraShake camInstance;
    public AnimationCurve curve;
    public float ShakeTime = 1.0f;

    //private void Awake()
    //{
        //camInstance = this;
    //}
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(StartShake());
        }
    }

    public void Shake()
    {
        StartCoroutine(StartShake());
    }
    public IEnumerator StartShake()
    {
        Vector3 StarPosition = transform.position;
        float TimeUsed = 0f;

        while(TimeUsed< ShakeTime)
        {
            TimeUsed += Time.deltaTime;
            float strenght = curve.Evaluate(TimeUsed / ShakeTime);
            transform.position = StarPosition+Random.insideUnitSphere * strenght;
            yield return null;
        }

        transform.position = StarPosition;
        Debug.Log("bbb");

    }
}
