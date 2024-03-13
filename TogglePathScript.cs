using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePathScript : MonoBehaviour
{
    /* Reference to UI TogglePath Components */
    public GameObject UI_TogglePath_GameObject;
    // Teleport Anchor Variable
    public GameObject hittingZoneAnchor;
    // XR Origin (XR Rig) Variable
    public GameObject xrOrigin;
    // Text Toggle Element Toggle Path
    public Toggle textToggleElementTogglePath;
    // Text Toggle Elements Switch Zone
    public Toggle textToggleElementsSwitchZone;
    // CueTrackerScript.cs
    public CueTrackerScript cueTrackerScript;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 hittingZone = hittingZoneAnchor.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Dynamic Bool: On Value Changed
    public void OnValueChanged_TogglePath(bool toggleIsOn)
    {
        if (toggleIsOn)
        {
            /* Show Path */
            // Debug.Log("Toggle is on");
        }
        if (toggleIsOn == false)
        {
            /* Hide Path */
            // Debug.Log("Toggle is off");
        }
    }

    // Dynamic Bool: On Value Changed
    public void OnValueChanged_SwitchZone(bool toggleIsOn)
    {
        if (toggleIsOn)
        {

            // Move from current position to hittingZone position
            xrOrigin.transform.position = hittingZoneAnchor.transform.position;
            // 將 cueTrackerScript 的 isStandOnHittingZone 等於 true
            cueTrackerScript.isStandOnHittingZone = true;
            if (textToggleElementTogglePath.isOn == true)
            {
                textToggleElementTogglePath.isOn = false;
            }
            if (textToggleElementsSwitchZone.isOn == true)
            {
                textToggleElementsSwitchZone.isOn = false;
            }
        }
    }
}
