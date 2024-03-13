using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipSyncScript : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Dynamic Bool: On Value Changed
    public void OnValueChanged(bool toggleIsOn)
    {
        Debug.Log("OnValueChanged: " + toggleIsOn);
        if (toggleIsOn)
        {
            Debug.Log("Toggle is on");
            // CamilaLipSync_GameObject appears
            CamilaLipSync_GameObject.SetActive(true);
            // Animator is enabled
            animator.enabled = true;
            // AudioSource is enabled
            audioSource.enabled = true;
            // AudioSource is playing
            audioSource.Play();
        }
        else
        {
            Debug.Log("Toggle is off");
        }
    }
}
