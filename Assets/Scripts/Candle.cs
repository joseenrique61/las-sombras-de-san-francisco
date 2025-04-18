using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour
{
    [SerializeField] private float duration = 120f;
    [SerializeField] private float timer;
    private Light2D candleLight;
    // Start is called before the first frame update
    void Start()
    {
        candleLight = GetComponent<Light2D>();
        timer = duration;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            candleLight.enabled = false;
        }
    }

    public void ReactiveCandleLight()
    {
        timer = duration;
        candleLight.enabled = true;
    }

}
