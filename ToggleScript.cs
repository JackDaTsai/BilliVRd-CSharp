using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.OpenXR.Input;

public class ToggleScript : MonoBehaviour
{
    /* Reference to UI Poke Components */
    public GameObject UI_Poke_Components_GameObject;
    /* Reference to UI TogglePath Components */
    public GameObject UI_TogglePath_GameObject;
    /* Reference to CamilaLipSync */
    public GameObject CamilaLipSync_GameObject;
    /* Reference to Animator */
    private Animator animator;
    /* Reference to AudioSource */
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        // Get Animator and AudioSource components
        animator = CamilaLipSync_GameObject.GetComponent<Animator>();
        audioSource = CamilaLipSync_GameObject.GetComponent<AudioSource>();
        // UI_TogglePath_GameObject disappears
        UI_TogglePath_GameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Dynamic Bool: On Value Changed
    public void OnValueChanged(bool toggleIsOn)
    {
        if (toggleIsOn)
        {
            // Dev Mode            
            // Debug.Log("Toggle is on");
            
            // UI_Poke_Components_GameObject disappears
            UI_Poke_Components_GameObject.SetActive(false);
            // Animator is disabled
            animator.enabled = false;
            // AudioSource is disabled
            audioSource.enabled = false;
            // AudioSource is not playing
            audioSource.Stop();
            // CamilaLipSync_GameObject disappears
            CamilaLipSync_GameObject.SetActive(false);
            // UI_TogglePath_GameObject appears
            UI_TogglePath_GameObject.SetActive(true);
        }
        else
        {
            // Dev Mode
            // Debug.Log("Toggle is off");
        }
    }
}
