using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {

    Vector3 offSet;
	// Use this for initialization
	void Start () {
        //set the offSet
        offSet = transform.position - GameObject.Find("Player").transform.position;

	}
	
	// Update is called once per frame
	void Update () {
        transform.position = GameObject.Find("Player").transform.position + offSet;
	}
}
