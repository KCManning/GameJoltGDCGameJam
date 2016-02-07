using UnityEngine;
using System.Collections;

public class Weapon_Script : MonoBehaviour {

    GameObject hand;
	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
        GameObject player = GameObject.Find("Player");
        Animator ani = player.GetComponent<Animator>();
        Transform hand = ani.GetBoneTransform(HumanBodyBones.LeftHand);
        transform.position = hand.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //write out collided with enemy
            print("Collided with enemy");
        }
    }
}
