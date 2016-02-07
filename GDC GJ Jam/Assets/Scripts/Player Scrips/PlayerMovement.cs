using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //bool canJump = false;
    Quaternion originalRotation;
    // bool isOnGround = false;
    float RelativeX = 0;
    float RelativeZ = 0;
    public float speed;
    public float max_z;
    public static bool isAttacking = false;
    public static float sand = 100;
    // Use this for initialization
    void Start()
    {
        //set the original transformation
        originalRotation = transform.rotation;

        Animator ani = GetComponent<Animator>();
        ani.Play("Default");
    }

    // Update is called once per frame
    void Update()
    {



        bool canJump = Physics.Raycast(transform.position, Vector3.down, 0.2f);
        //Debug.DrawRay(transform.position, Vector3.down*10,Color.green);
        bool isOnGround = canJump;

        bool noKey = true;
        bool noRightKey = true;
        //get rigid body
        Rigidbody body = GetComponent<Rigidbody>();

        Animator ani = GetComponent<Animator>();
        if (Input.GetKey(KeyCode.A) && isOnGround)
        {
            RelativeX = -speed;
            //rotate the player
            transform.rotation = originalRotation;
            transform.Rotate(new Vector3(0, 180, 0));
            noKey = false;
            ani.SetBool("Running", true);
            ani.SetBool("Jumping", false);
            ani.SetBool("Attacking", false);
            isAttacking = false;
        }


        if (Input.GetKey(KeyCode.D) && isOnGround)
        {
            RelativeX = speed;
            //rotate the player
            //rotate the player
            transform.rotation = originalRotation;
            //transform.Rotate(new Vector3(0, 0, 0));
            noKey = false;
            ani.SetBool("Running", true);
            ani.SetBool("Jumping", false);
            ani.SetBool("Attacking", false);
            isAttacking = false;
        }



        if (noKey && isOnGround)
        {
            //set the relative x to 0
            RelativeX = 0;
            ani.SetBool("Jumping", false);
            ani.SetBool("Running", false);
            isAttacking = false;
        }


        //check to see if the player can jump
        if (canJump && Input.GetKey(KeyCode.Space))
        {
            Vector3 up = new Vector3(0, 1, 0);
            body.AddForce(up * 250, ForceMode.Acceleration);
            canJump = false;
            ani.SetBool("Jumping", true);
            ani.SetBool("Running", false);
            ani.SetBool("Attacking", false);
            isAttacking = false;

        }

        if(Input.GetKey(KeyCode.F))
        {
            //make the player attack
            ani.SetBool("Attacking",true);
            ani.SetBool("Running", false);
            ani.SetBool("Jumping", false);
            isAttacking = true;
        }


        //make sure the player does not fall infinitly
        if (transform.position.y < -10)
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

        if (isOnGround && noRightKey)
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

        //update sand amounts
        if(Input.GetKeyDown(KeyCode.I))
        {
            sand -= 10;
            if(sand < 0)
            {
                sand = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            sand += 10;
        }

        //update particle system opacity and speed to match the sand
        ParticleSystem system = GetComponent<ParticleSystem>();
        system.startColor = new Color(system.startColor.r, system.startColor.g, system.startColor.b, sand / 100);



    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Enemy")
        {

            //destroy some of the players sand
            sand -= sand * 0.05f;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.collider.tag == "JumpableSurface")
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

