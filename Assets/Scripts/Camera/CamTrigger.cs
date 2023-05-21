using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public Vector3 newCamPos, newPlayerPos;
    CamControl camControl;
    void Start()
    {
        camControl = Camera.main.GetComponent<CamControl>();
    }

   private void onTriggerEnter2D(Collider2D other)
   {
    if(other.gameObject.tag == "Player")
    {
        camControl.minPos += newCamPos;
        camControl.maxPos += newCamPos;

        other.transform.position += newPlayerPos;
    }
   }
   
}
