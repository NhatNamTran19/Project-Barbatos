using System.Collections;
using System.Collections.Generic;
using UnityEngine;using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTime;
    private float shakeTimeTotal;
    private float startIntensity;

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        startIntensity = intensity;
        shakeTimeTotal = time;
        shakeTime = time;
    }
    private void Update()
    {
        if (shakeTime > 0)
        {
            //Debug.Log(Mathf.Lerp(startIntensity, 0f, shakeTime / shakeTimeTotal));


            shakeTime -= Time.deltaTime;
            if (shakeTime <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
                //Mathf.Lerp(startIntensity, 0f, shakeTime / shakeTimeTotal);
        }
    }
}
