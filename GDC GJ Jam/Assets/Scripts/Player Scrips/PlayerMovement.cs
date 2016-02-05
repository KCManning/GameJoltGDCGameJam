using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    bool canJump = false;
    Quaternion originalRotation;
    bool isOnGround = false;
    float RelativeX = 0;
    public float speed;
	// Use this for initialization
	void Start () {
        //set the original transformation
        originalRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

        bool noKey = true;
        //get rigid body
        Rigidbody body = GetComponent<Rigidbody>();
        
        //check to see if the player can jump
        if(canJump && Input.GetKey(KeyCode.Space))
        {
            Vector3 up = new Vector3(0, 1, 0);
            body.AddForce(up * 300, ForceMode.Acceleration);
            canJump = false;
            

        }
        if(Input.GetKey(KeyCode.A)&& isOnGround)
        {
            RelativeX = -speed;
            //rotate the player
            transform.rotation = originalRotation;
            transform.Rotate(new Vector3(0, 0, 180));
            noKey = false;
        }
        

        if(Input.GetKey(KeyCode.D)&& isOnGround)
        {
            RelativeX = speed;
            //rotate the player
            //rotate the player
            transform.rotation = originalRotation;
            //transform.Rotate(new Vector3(0, 0, 0));
            noKey = false;
        }

        if(noKey && isOnGround)
        {
            //set the relative x to 0
            RelativeX = 0;
        }

        Vector3 newVector = transform.position;
        newVector.x += RelativeX;
        transform.position = newVector;

        //make sure the player does not fall infinitly
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
            isOnGround = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(col.collider.tag == "JumpableSurface")
        {
            //player is no longer colliding with the ground so prevent him from jumping
            canJump = false;
            isOnGround = false;
        }
    }

    void teleToSpawn()
    {
        //telepor the player back to the player spawn\
        Vector3 spawnPos = GameObject.Find("Player Spawn").transform.position;
        transform.position = spawnPos;
    }
}
