using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeInteraction : MonoBehaviour
{
    public Rigidbody2D rb;
    private HingeJoint2D hj;

    public float pushForce = 10f;

    public bool attached = false;
    public Transform attachedTo;
    private GameObject disregard;

    public GameObject pulleySelected = null;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        hj = gameObject.GetComponent<HingeJoint2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckKeyboardInputs();
        //CheckPulleyInputs();
    }

    /*              KEYBOARD CONTROLS
    * a or left arrow       = swing left on rope
    * d or right arrow      = swing right on rope
    * space                 = detach from rope
    * w or up arrow         = climb up rope
    * s or down arrow       = climb down rope
    */

    void CheckKeyboardInputs()
    {
        if(Input.GetKey("a"))
        {
            if(attached)
                rb.AddRelativeForce(new Vector3(-1, 0, 0) * pushForce);
        }

        if(Input.GetKey("d"))
        {
            if(attached)
                rb.AddRelativeForce(new Vector3(1, 0, 0) * pushForce);
        }

        
        if(attached && Input.GetKeyDown("w"))
            Slide(1);
        
        if(attached && Input.GetKeyDown("s") )
            Slide(-1);
        
        
        if(attached && Input.GetKeyDown(KeyCode.Space))
            Detach();
    }

    public void Attach(Rigidbody2D ropeBone)
    {
        ropeBone.gameObject.GetComponent<RopeSegment>().isPlayerAttached = true;
        hj.connectedBody = ropeBone;
        hj.enabled = true;
        attached = true;
        attachedTo = ropeBone.gameObject.transform.parent;
    }

    void Detach()
    {
        
        hj.connectedBody.gameObject.GetComponent<RopeSegment>().isPlayerAttached = false;
        attached = false;
        hj.enabled = false;
        hj.connectedBody = null;

        StartCoroutine(AttachedNull());
    }

    IEnumerator AttachedNull() 
    {
        //Default attachment cooldown 0.5f
        yield return new WaitForSeconds(3f);
        attachedTo = null;

    }



    public void Slide(int direction)
    {
        RopeSegment myConnection = hj.connectedBody.gameObject.GetComponent<RopeSegment>();
        GameObject newSeg = null;

        if(direction > 0)
        {
            if(myConnection.connectedAbove != null)
            {
                if(myConnection.connectedAbove.gameObject.GetComponent<RopeSegment>() != null)
                    newSeg = myConnection.connectedAbove;
            }
        }
        else 
        {
            if(myConnection.connectedBelow != null)
                newSeg = myConnection.connectedBelow;
        }

        if(newSeg != null)
        {
            transform.position = newSeg.transform.position;
            myConnection.isPlayerAttached = false;
            newSeg.GetComponent<RopeSegment>().isPlayerAttached = true;
            hj.connectedBody = newSeg.GetComponent<Rigidbody2D>();
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!attached)
        {
            if(col.gameObject.tag == "Rope")
            {
                if(attachedTo != col.gameObject.transform.parent)
                {
                    if(disregard == null || col.gameObject.transform.parent.gameObject != disregard)
                        Attach(col.gameObject.GetComponent<Rigidbody2D>());
                }
            }
        }
    }
}
