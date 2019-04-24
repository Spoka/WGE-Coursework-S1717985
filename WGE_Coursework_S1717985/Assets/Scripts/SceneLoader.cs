using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void LoadScene1()
    {
        SceneManager.LoadScene("Scene 1");
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene("Scene 2");
    }
}
