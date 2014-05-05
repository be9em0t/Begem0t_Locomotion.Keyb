using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {
    
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	if (Input.GetKeyDown(KeyCode.C))
    {
        CamSwitch();
    }

    }

    void CamSwitch()
    {
        Camera[] allCams = FindObjectsOfType(typeof(Camera)) as Camera[];

        for (int i = 0; i < allCams.Length; i++)
        {
            if (allCams[i].enabled == true)
            {
                //Debug.Log("length:" + allCams.Length + " : index: " + i + " : " + allCams[i].name);

                allCams[i].enabled = false;
                if (i == allCams.Length - 1)
                {
                    allCams[0].enabled = true;
                    Debug.Log("Switched to camera " + allCams[0].name);
                }
                else
                {
                    allCams[i + 1].enabled = true;
                    Debug.Log("Switched to camera " + allCams[i + 1].name);
                }
                break;
            }

        }
    }
}
