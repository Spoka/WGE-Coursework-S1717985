using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeDialogueTrigger : MonoBehaviour {

    public WeeDialogueBit dialogue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialogue.NPCPhrase();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialogue.dialoguePanel.SetActive(false);
        }
    }
}
