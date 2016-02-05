using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    bool canJump;
    Quaternion originalRotation;
	// Use this for initialization
	void Start () {
        //set the original transformation
        originalRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

        //get rigid body
        Rigidbody body = GetComponent<Rigidbody>();
        
        //check to see if the player can jump
        if(canJump && Input.GetKey(KeyCode.Space))
        {
            Vector3 up = new Vector3(0, 1, 0);
            body.AddForce(up * 300, ForceMode.Acceleration);
            canJump = false;

        }
        float newXVel = 0;
        if(Input.GetKey(KeyCode.A))
        {
            newXVel = -15;
            //rotate the player
            transform.rotation = originalRotation;
            transform.Rotate(new Vector3(0, 0, 180));
            

        }

        if(Input.GetKey(KeyCode.D))
        {
            newXVel = 15;
            //rotate the player
            //rotate the player
            transform.rotation = originalRotation;
            //transform.Rotate(new Vector3(0, 0, 0));
        }

        //apply motion
        Vector3 newVelocity = body.velocity;
        newVelocity.x = newXVel;
        body.velocity = newVelocity;
        if(transform.position.y < -10)
        {
            teleToSpawn();
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag =="JumpableSurface")
        {
            canJump = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(col.collider.tag == "JumpableSurface")
        {
            //player is no longer colliding with the ground so prevent him from jumping
            canJump = false;
        }
    }

    void teleToSpawn()
    {
        //telepor the player back to the player spawn\
        Vector3 spawnPos = GameObject.Find("Player Spawn").transform.position;
        transform.position = spawnPos;
    }
}
