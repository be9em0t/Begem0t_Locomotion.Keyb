using UnityEngine;
using System.Collections;

public class b9CameraConstant : MonoBehaviour {

    //public Transform target;        // The target object we are following
    public Transform avatarTransf;         //target avatar's transform

    float camLockHeight = 1.8f;
    float lookLockHeight = 0.85f;
    float camDistance = 2.6f;
    
    //float camDist = -2.5f;          //Initial Camera distance
    //float camHeight = 1f;          //Initial Camera height
    //float rotAround = 0f;           //cameraParent rotation around target
    //float vertOffset = -1f;          //camera vertial offset
    //float cameraZoom = 60f;         //camera FieldOfView

    Vector3 camLock;              //lerp-to camera positon
    Vector3 lookLock;              //lerp-to camera lookAt position
    Vector3 lookTarget;             //camera lookAt target position

    float dampCamLock = 3f;              //dampen camera positon
    float dampLookLock = 2f;              //dampen camera lookAt position

	// Use this for initialization
	void Start () {
        //define target object
        if (avatarTransf == null)   //if no target defined
        {
            Debug.Log("Error: no camera target assigned. Assuming Default Avatar.");    
            avatarTransf = GameObject.Find("DefaultAvatar").transform;                  //get target avatar's transform
        }

        //create lookAt target value
        lookTarget = new Vector3(avatarTransf.position.x, lookLockHeight, avatarTransf.position.z);

    }
	
	// Update is called once per frame
	void Update () {

        //positon camera 
        camLock = new Vector3(avatarTransf.position.x, camLockHeight, avatarTransf.position.z - camDistance);
        transform.position = Vector3.Lerp(transform.position, camLock, Time.deltaTime * dampCamLock);

        //positon camera look-at target
        lookLock = new Vector3(avatarTransf.position.x, lookLockHeight, avatarTransf.position.z);
        lookTarget = Vector3.Lerp(lookTarget, lookLock, Time.deltaTime * dampLookLock);
        transform.LookAt(lookTarget); 

    }

}
