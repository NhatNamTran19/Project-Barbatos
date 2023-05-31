using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField]
    public GameObject camera;
    [SerializeField]
    public float parallaxEffect;
    private float length;
    private float startPoint; 
    void Start()
    {
        startPoint = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (camera.transform.position.x * (1-parallaxEffect));
        float dist = (camera.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPoint + dist, transform.position.y, transform.position.z);
        if (temp >= startPoint+length) startPoint += length;
        else if (temp <= startPoint - length) startPoint -= length;


    }
}
