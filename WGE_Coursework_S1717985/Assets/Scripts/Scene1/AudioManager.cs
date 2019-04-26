using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip destroyGrassBlockSound;
    public AudioClip destroyDirtBlockSound;
    public AudioClip destroySandBlockSound;
    public AudioClip destroyStoneBlockSound;
    public AudioClip placeGrassBlockSound;
    public AudioClip placeDirtBlockSound;
    public AudioClip placeSandBlockSound;
    public AudioClip placeStoneBlockSound;
    public VoxelChunk voxelChunk;
    public PlayerScript playerScript;

    void PlayDestroyBlockSound()
    {
        if (playerScript.bType == 1)
        {
            GetComponent<AudioSource>().PlayOneShot(destroyGrassBlockSound);
        }
        if (playerScript.bType == 2)
        {
            GetComponent<AudioSource>().PlayOneShot(destroyDirtBlockSound);
        }
        if (playerScript.bType == 3)
        {
            GetComponent<AudioSource>().PlayOneShot(destroySandBlockSound);
        }
        if (playerScript.bType == 4)
        {
            GetComponent<AudioSource>().PlayOneShot(destroyStoneBlockSound);
        }
    }

    void PlayPlaceBlockSound()
    {
        if(voxelChunk.blockToPlace == 1)
        {
            GetComponent<AudioSource>().PlayOneShot(placeGrassBlockSound);
        }
        if (voxelChunk.blockToPlace == 2)
        {
            GetComponent<AudioSource>().PlayOneShot(placeDirtBlockSound);
        }
        if (voxelChunk.blockToPlace == 3)
        {
            GetComponent<AudioSource>().PlayOneShot(placeSandBlockSound);
        }
        if (voxelChunk.blockToPlace == 4)
        {
            GetComponent<AudioSource>().PlayOneShot(placeStoneBlockSound);
        }
    }

    void OnEnable()
    {
        VoxelChunk.OnEventBlockDestroyed += PlayDestroyBlockSound;
        VoxelChunk.OnEventBlockPlaced += PlayPlaceBlockSound;
    }

    void OnDisable()
    {
        VoxelChunk.OnEventBlockDestroyed -= PlayDestroyBlockSound;
        VoxelChunk.OnEventBlockPlaced -= PlayPlaceBlockSound;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
