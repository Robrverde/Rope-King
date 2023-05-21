using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheck : MonoBehaviour
{
    [SerializeField] GameObject DialogueBox;
    [SerializeField] PlayerMovement Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.tag == "Player")
        if(DialogueBox)
        {
            DialogueBox.SetActive(true);
            Player.horizontal = 0f;
        }
            
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.gameObject.SetActive(false);
    }
}
