using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour {

	public void LoadScene1()
    {
        Application.LoadLevel("Scene 1");
    }

    public void LoadScene2()
    {
        Application.LoadLevel("Scene 2");
    }
}
