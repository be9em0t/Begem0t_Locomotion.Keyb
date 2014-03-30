using UnityEngine;
using System.Collections;

public class b9Camera2 : MonoBehaviour {
    private float newIntensity;

    public Transform target;    // The target object we are following

    public float camLockHeight = 1.8f;
    public float lookLockHeight = 0.85f;
    public float camDistance = 2.6f;
    //public float dampCamDistance = 0.01f;
    
    // Dampen
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    //sub-lerps
    //float camLockDist;
    //float camLockSide;
    //public float dampCamLockDist = 3f; //bigger = faster
    //public float dampCamLockSide =.1f;

    //float DampCamera = 2f;	        //Adjust smoothing of camera motion: 0 - infinity, 10 almost instant, default 2
    //float camDist = -2.5f;          //Initial Camera distance
    //float camHeight = 1f;          //Initial Camera height
    //float rotAround = 0f;           //cameraParent rotation around target
    //float vertOffset = -1f;          //camera vertial offset
    //float cameraZoom = 60f;         //camera FieldOfView

    //public GameObject cameraParent;     //camera's parent object
    public GameObject cameraLookAt;     //camera's parent object

    Transform avatarTransf;         //target avatar's transform
    Vector3 camLock;              //lerp-to camera positon
    Vector3 lookLock;              //lerp-to camera lookAt position
    public float dampCamLock = 2f;              //dampen camera positon
    public float dampLookLock = 2f;              //dampen camera lookAt position

	// Use this for initialization
	void Start () {
        if (target == null)
        {
            Debug.Log("Error: no camera target assigned. Assuming Default Avatar.");          // Error if no camera target assigned
            avatarTransf = GameObject.Find("DefaultAvatar").transform;  //get target avatar's transform
        }
        else avatarTransf = target.transform;

        //cameraParent = new GameObject("cameraParent");              //create camera's parent object at 0,0,0
        cameraLookAt = new GameObject("cameraLookAt");              //create camera's lookAt object at 0,0,0
        //transform.parent = cameraParent.transform;                  //parent camera to cameraParent, so that it follows avatar
	}
	
	// Update is called once per frame
	void Update () {

        //targetPos = new Vector3(avatarTransf.position.x, avatarTransf.position.y - vertOffset, avatarTransf.position.z);
        //cameraParent.transform.position = Vector3.Lerp(cameraParent.transform.position, targetPos, DampCamera * Time.deltaTime);
        //cameraParent.transform.position = Vector3.Lerp(cameraParent.transform.position, targetPos, DampCamera * Time.deltaTime);

        //Mathf.MoveTowards(avatarTransf.position.z, avatarTransf.position.z - camDistance, camLockDist)

        //camLockDist = Mathf.Lerp(camLockDist, avatarTransf.position.z - 2.5f, Time.deltaTime * dampCamLockDist);
        //camLockSide = Mathf.Lerp(camLockSide, avatarTransf.position.x, Time.deltaTime * dampCamLockSide);
        //camLock = new Vector3(camLockSide, camLockHeight, camLockDist);

        camLock = new Vector3(avatarTransf.position.x, camLockHeight, avatarTransf.position.z - camDistance);
        lookLock = new Vector3(avatarTransf.position.x, lookLockHeight, avatarTransf.position.z);
        
        //Direct mode
        //transform.position = camLock; //camera position
        //transform.LookAt(lookLock); //camera lookAt

        cameraLookAt.transform.position = Vector3.Lerp(cameraLookAt.transform.position,  avatarTransf.transform.position, Time.deltaTime * dampLookLock);
        //lerp3 the whole position
        transform.position = Vector3.Lerp(transform.position, camLock, Time.deltaTime * dampCamLock);
        transform.LookAt(cameraLookAt.transform.position);
        //transform.LookAt(Vector3.Lerp(transform.position, lookLock, Time.deltaTime * dampLookLock));

        //var rotation = Quaternion.LookRotation(target.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampLookLock);
  

        //cameraParent.transform.position = Vector3.Lerp(transform.position, camLock, .05f);
        //cameraParent.transform.rotation = avatarTransf.transform.rotation;

        // Set the height of the camera
        //transform.position = new Vector3(transform.position.x, height, transform.position.z);

        // Always look at the target
        //transform.LookAt(target);

    }

}
