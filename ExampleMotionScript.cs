using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleMotionScript : MonoBehaviour
{
    // Reference to the RL_Motion GameObject
    public GameObject RL_MotionGameObject;
    // Reference to the RL_Motion Animator
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Get the RL_Motion Animator
        animator = RL_MotionGameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RL_MotionGameObject.activeSelf && animator != null && animator.enabled)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("MotionNewest_Imported_2987704582080_TempMotion") || animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                animator.Play("MotionNewest_Imported_2987704582080_TempMotion", -1, 0f);
            }
        }

        if (RL_MotionGameObject.activeSelf && animator != null && animator.enabled)
        {
            RL_MotionGameObject.transform.Rotate(0, 45 * Time.deltaTime, 0);
        }
    }

    // Dynamic Bool: On Value Changed
    public void OnValueChanged(bool toggleIsOn)
    {
        // Dev Mode
        // Debug.Log("OnValueChanged: " + toggleIsOn);
        if (toggleIsOn)
        {            
            // RL_MotionGameObject appears
            RL_MotionGameObject.SetActive(true);
            // Animator is enabled
            animator.enabled = true;
        }
        else
        {
            // Animator is disabled
            animator.enabled = false;
            // RL_MotionGameObject disappears
            RL_MotionGameObject.SetActive(false);
        }
    }
}
