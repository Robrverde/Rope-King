using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPhaseControl : MonoBehaviour
{
    public float timeTophase = 2;
    public float currentTime = 0;
    public bool enable = true;
   
    void Start()
    {
        enable = true;   
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= timeTophase)
        {
            currentTime = 0;
            TogglePlatform();
        }
        
    }

    void TogglePlatform()
    {
        enable = !enable;

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(enable);            
        }
    }

    
}
