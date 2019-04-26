using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeDialogueBit : MonoBehaviour {

    public GameObject dialoguePanel;
    public GameObject playerText;
    public GameObject npcText;
    public GameObject nextButton;

	// Use this for initialization
	void Start () {
        dialoguePanel.SetActive(false);
	}
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerPhrase()
    {
        playerText.SetActive(true);
        npcText.SetActive(false);
        nextButton.SetActive(false);
    }

    public void NPCPhrase()
    {
        dialoguePanel.SetActive(true);
        playerText.SetActive(false);
    }
}
