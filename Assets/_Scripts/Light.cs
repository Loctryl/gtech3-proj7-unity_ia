using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light : MonoBehaviour
{

    [SerializeField]
    private Light2D light2d;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        light2d.intensity = Mathf.PingPong(4, 5.5f);
        light2d.color = Color.Lerp(Color.red, Color.yellow, Mathf.PingPong(Time.time, 1));
 
        light2d.shapeLightFalloffSize = Mathf.PingPong(Time.time, 1);
       
    }
}
