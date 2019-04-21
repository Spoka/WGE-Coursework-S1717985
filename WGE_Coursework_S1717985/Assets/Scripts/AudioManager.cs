using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip destroyBlockSound;
    public AudioClip placeBlockSound;

    void PlayDestroyBlockSound()
    {
        GetComponent<AudioSource>().PlayOneShot(destroyBlockSound);
    }

    void PlayPlaceBlockSound()
    {
        GetComponent<AudioSource>().PlayOneShot(placeBlockSound);
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
