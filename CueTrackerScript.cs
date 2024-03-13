using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CueTrackerScript : MonoBehaviour
{
    public GameObject leftHandDirectInteractor;
    public GameObject rightHandDirectInteractor;
    public GameObject cueInstance;    
    public Transform cueInstanceTransform;
    public GameObject xrOrigin;
    public Ready2PlayScript_20240301 ready2PlayScript;
    public SerialPortCommunicationScript_F arduinoDevScript;
    private float xThreshold = 0.123f;
    private float heightThreshold = 0.123f;
    private float zThreshold = 0.0123f;
    public bool isStandOnHittingZone = false;
    // Count Play Time
    // private int countPlayTime = 0;

    void Start()
    {
        cueInstance.SetActive(false);
    }

    void Update()
    {
        // Block of code to track the left hand position
        float leftHand_x = leftHandDirectInteractor.transform.position.x;
        float leftHandHeight = leftHandDirectInteractor.transform.position.y;
        float leftHand_z = leftHandDirectInteractor.transform.position.z;
        // Block of code to track the right hand position
        float rightHand_x = rightHandDirectInteractor.transform.position.x;
        float rightHandHeight = rightHandDirectInteractor.transform.position.y;
        float rightHand_z = rightHandDirectInteractor.transform.position.z;
        
        // Block of code to track the both hands position
        bool handsWithinThreshold_x = Mathf.Abs(leftHand_x - rightHand_x) < xThreshold;
        bool handsWithinThreshold_y = Mathf.Abs(leftHandHeight - rightHandHeight) < heightThreshold;
        bool handsWithinThreshold_z = Mathf.Abs(leftHand_z - rightHand_z) < zThreshold;

        // y axis is the height of the player & x axis is the width of the player
        if (isStandOnHittingZone && handsWithinThreshold_y)
        {
            cueInstance.SetActive(true);
            cueInstanceTransform.position = new Vector3(rightHandDirectInteractor.transform.position.x + 0.0123f, rightHandDirectInteractor.transform.position.y - 0.0123f, rightHandDirectInteractor.transform.position.z + 1.23f);
        }
        else
        {
            cueInstance.SetActive(false);
        }

        // log
        // Debug.Log("leftHandHeightDirectInteractor: " + leftHandDirectInteractor);
        // Debug.Log("rightHandHeightDirectInteractor: " + rightHandDirectInteractor);
    }
}
