using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class KU_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(transform.position.y < -10)
        {
            //go back to main menu
            SceneManager.LoadScene("Credits");
        }
	}

    void OnCollisionEnter(Collision other)
    {
        //if the other is a player then kill self
        if(other.collider.tag != "JumpableSurface")
        {
            Animator ani = GetComponent<Animator>();
            ani.SetFloat("Sand", 0);
        }
    }
}
