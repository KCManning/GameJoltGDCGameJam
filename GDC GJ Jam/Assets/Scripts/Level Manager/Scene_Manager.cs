using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour {

    public float autoLoadNextSceneAfter;

	// Use this for initialization
	void Start () {
        if(autoLoadNextSceneAfter != 0) { Invoke("LoadNextScene", autoLoadNextSceneAfter); }
	
	}
	
    public void LoadScene(string name)
    {
        Debug.Log("New Scene Load: " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitRequest()
    {
        Debug.Log("Quit Requested");
        Application.Quit();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Weapon")
            LoadNextScene();
    }

}
