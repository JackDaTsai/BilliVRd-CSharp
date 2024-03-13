using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchZoneScript : MonoBehaviour
{
    // Teleport Anchor Variable
    private GameObject teleportAnchorStart;
    public GameObject hittingZoneAnchor;
    // Teleport Anchor Variable
    private GameObject teleportAnchorEnd;
    public GameObject tableZoneAnchor;
    // XR Origin (XR Rig) Variable
    public GameObject xrOrigin;
    // Analyze Anchor Variable
    private GameObject analyzeAnchor;
    public GameObject analyzeZoneAnchor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // InputAction CallbackContext
    public void AButtonPressed(InputAction.CallbackContext context)
    {
        // Get the position of teleportAnchorStart
        Vector3 teleportAnchorStartPosition = teleportAnchorStart.transform.position;
        // Get the position of teleportAnchorEnd
        Vector3 teleportAnchorEndPosition = teleportAnchorEnd.transform.position;
        // Get the position of xrOrigin
        Vector3 xrOriginPosition = xrOrigin.transform.position;
        // If the teleport button is pressed
        if (context.started)
        {
            // Move from current position to teleportAnchorEnd position
            xrOrigin.transform.position = teleportAnchorEndPosition;
        }
        // If the teleport button is released
        //if (context.canceled)
        //{
        //    xrOrigin.transform.position = teleportAnchorStartPosition;
        //}
    }

    public void AutoSwitchTableZoneMethod()
    {
        // Get the position of tableZoneAnchor
        Vector3 tableZoneAnchorPosition = tableZoneAnchor.transform.position;
        // Get the position of xrOrigin
        Vector3 xrOriginPosition = xrOrigin.transform.position;

        // Get the position of tableZoneAnchor
        Debug.Log("tableZoneAnchorPosition: " + tableZoneAnchorPosition);
        // Get the position of xrOrigin
        Debug.Log("xrOriginPosition: " + xrOriginPosition);

        // Move from current position to tableZoneAnchor position
        xrOrigin.transform.position = tableZoneAnchorPosition;
    }

    public void AutoSwitchHittingZoneMethod()
    {
        // Get the position of hittingZoneAnchor
        Vector3 hittingZoneAnchorPosition = hittingZoneAnchor.transform.position;
        // Get the position of xrOrigin
        Vector3 xrOriginPosition = xrOrigin.transform.position;

        // Get the position of hittingZoneAnchor
        Debug.Log("hittingZoneAnchorPosition: " + hittingZoneAnchorPosition);
        // Get the position of xrOrigin
        Debug.Log("xrOriginPosition: " + xrOriginPosition);

        // Move from current position to hittingZoneAnchor position
        xrOrigin.transform.position = hittingZoneAnchorPosition;
    }


    public void AutoSwitchAnalyzeZoneMethod()
    {
        // Get the position of analyzeZoneAnchor
        Vector3 analyzeZoneAnchorPosition = analyzeZoneAnchor.transform.position;
        // Get the position of xrOrigin
        Vector3 xrOriginPosition = xrOrigin.transform.position;

        // Get the position of analyzeZoneAnchor
        Debug.Log("analyzeZoneAnchorPosition: " + analyzeZoneAnchorPosition);
        // Get the position of xrOrigin
        Debug.Log("xrOriginPosition: " + xrOriginPosition);

        // Move from current position to analyzeZoneAnchor position
        xrOrigin.transform.position = analyzeZoneAnchorPosition;
    }
}
