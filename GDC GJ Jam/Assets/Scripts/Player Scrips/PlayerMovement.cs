using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    //bool canJump = false;
    Quaternion originalRotation;
   // bool isOnGround = false;
    float RelativeX = 0;
    float RelativeZ = 0;
    public float speed;
    public float max_z;

    Animator ani;

    static private bool foot;
	// Use this for initialization
	void Start () {
        //set the original transformation
        originalRotation = transform.rotation;

        Animator ani = GetComponent<Animator>();
        ani.Play("Default");
        foot = true;
	}
	
	// Update is called once per frame
	void Update () {

        bool canJump = Physics.Raycast(transform.position, Vector3.down,0.2f);
        //Debug.DrawRay(transform.position, Vector3.down*10,Color.green);
        bool isOnGround = canJump;

        bool noKey = true;
        bool noRightKey = true;
        //get rigid body
        Rigidbody body = GetComponent<Rigidbody>();

        if(Input.GetKey(KeyCode.A)&& isOnGround)
        {
            RelativeX = -speed;
            //rotate the player
            transform.rotation = originalRotation;
            transform.Rotate(new Vector3(0, 180, 0));
            noKey = false;
            walking();
        }
        

        if(Input.GetKey(KeyCode.D)&& isOnGround)
        {
            RelativeX = speed;
            //rotate the player
            //rotate the player
            transform.rotation = originalRotation;
            //transform.Rotate(new Vector3(0, 0, 0));
            noKey = false;
            walking();

        }

        //check to see if the player can jump
        if (canJump && Input.GetKey(KeyCode.Space))
        {
            Vector3 up = new Vector3(0, 1, 0);
            body.AddForce(up * 250, ForceMode.Acceleration);
            canJump = false;
            ani.Play("Jumping");
            //ani.SetBool("Walking", false);


        }

        if (noKey && isOnGround)
        {
            //set the relative x to 0
            RelativeX = 0;
        }

        

        //make sure the player does not fall infinitly
        if(transform.position.y < -10)
        {
            teleToSpawn();
        }

        //move the player by the zcoord
        if (Input.GetKey(KeyCode.S) && isOnGround)
        {
            RelativeZ = -speed;
            //rotate the player
            noRightKey = false;
        }


        if (Input.GetKey(KeyCode.W) && isOnGround)
        {
            RelativeZ = speed;
            //rotate the player
            //rotate the player
            noRightKey = false;
        }

        if(isOnGround && noRightKey)
        {
            RelativeZ = 0;
            //ani.SetBool("Idle", true);
        }
        


        if (transform.position.z < -max_z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -max_z);
        }
        else if (transform.position.z > max_z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, max_z);
        }

        Vector3 newVector = transform.position;
        newVector.x += RelativeX;
        newVector.z += RelativeZ;
        transform.position = newVector;



    }

    void walking()
    {
        if (foot)
        {
            ani.Play("RightStep");
            foot = false;
        }
        else
        {
            ani.Play("LeftStep");
            foot = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag =="JumpableSurface")
        {
      //      canJump = true;
        //    isOnGround = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(col.collider.tag == "JumpableSurface")
        {
            //player is no longer colliding with the ground so prevent him from jumping
        //    canJump = false;
           // isOnGround = false;
        }
    }

    void teleToSpawn()
    {
        //telepor the player back to the player spawn\
        Vector3 spawnPos = GameObject.Find("Player Spawn").transform.position;
        transform.position = spawnPos;
    }

}
