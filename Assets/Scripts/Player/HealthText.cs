using System.Collections;
using System.Collections.Generic;
using UnityEngine;using TMPro;

public class HealthText : MonoBehaviour
{
    [SerializeField] private Vector3 moveSpeed = new Vector3(0,75,0);
    [SerializeField] private float timeToFade = 1f;

    private float timeElapsed=0f;
    private Color startColor;


    RectTransform textTranform;
    TextMeshProUGUI textMeshPro;
    void Start()
    {
        textTranform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        textTranform.position += moveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;

        if(timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            textMeshPro.color = new Color(startColor.r,startColor.g,startColor.b, fadeAlpha);          
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
