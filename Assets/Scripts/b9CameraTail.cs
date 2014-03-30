// Smooth Follow from Standard Assets
// Converted to C# because I fucking hate UnityScript and it's inexistant C# interoperability
// If you have C# code and you want to edit SmoothFollow's vars ingame, use this instead.
using UnityEngine;
using System.Collections;

public class b9CameraTail : MonoBehaviour
{

    // The target we are following
    public Transform target;
    Vector3 targetLookAt;
    Vector3 wantedCameraPosition;

    // The distance in the x-z plane to the target
    public float distance = 2.2f;
    // the height we want the camera to be above the target
    public float heightCamera = 1.8f;
    public float heightTarget = .85f;
    public float distanceCamera = .85f;
    // How much we 
    public float heightDamping = 2.0f;
    public float translateDamping = 1.5f;
    public float rotationDamping = .5f;

    // Place the script in the Camera-Control group in the component menu
    [AddComponentMenu("Camera-Control/Smooth Follow")]

    // Use this for initialization
    void Start()
    {
        //define target object
        if (target == null)   //if no target defined
        {
            Debug.Log("Error: no camera target assigned. Assuming Default Avatar.");
            target = GameObject.Find("Hips").transform;                  //get target avatar's transform
        }

        //create lookAt target value
        //lookTarget = new Vector3(avatarTransf.position.x, lookLockHeight, avatarTransf.position.z);

    }

    void LateUpdate()
    {
        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        //float wantedCameraHeight = target.position.y + heightCamera;
        //float wantedTargetHeight = target.position.y + heightTarget;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        targetLookAt = new Vector3(target.position.x, heightTarget, target.position.z);
        //wantedCameraPosition = new Vector3();

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        //currentHeight = Mathf.Lerp(currentHeight, wantedCameraHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        transform.position = target.position;

        wantedCameraPosition = new Vector3(target.position.x, target.position.y, target.position.z);
        transform.position = Vector3.Lerp(transform.position, wantedCameraPosition, Time.deltaTime * translateDamping);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        //transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        transform.position = transform.position  - (currentRotation * Vector3.forward * distance);


        //transform.rotation = currentRotation;

        //// Set the height of the camera

        // Fixed height of the camera
        //transform.position = new Vector3(transform.position.x, heightCamera, transform.position.z);

        // Always look at the target
        //transform.LookAt(target);
        transform.LookAt(targetLookAt);
    }
}