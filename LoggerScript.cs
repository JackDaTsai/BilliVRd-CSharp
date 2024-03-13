using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // hover Entered
    public void hoverEntered()
    {
        Debug.Log("Hover Entered");
    }

    // hover Exited
    public void hoverExited()
    {
        Debug.Log("Hover Exited");
    }

    // select Entered
    public void selectEntered()
    {
        Debug.Log("Select Entered");
    }

    // select Exited
    public void selectExited()
    {
        Debug.Log("Select Exited");
    }

    // activated
    public void activated()
    {
        Debug.Log("Activated");
    }

    // deactivated
    public void deactivated()
    {
        Debug.Log("Deactivated");
    }

    // X Button Pressed
    public void xButtonPressed(InputAction.CallbackContext context)
    {
        // if (context.started)
        // {
        //     Debug.Log("[Action: X Button Pressed] - Started");
        // }
        if (context.performed)
        {
            Debug.Log("[Action: X Button Pressed] - Performed");
        }
        // if (context.canceled)
        // {
        //     Debug.Log("[Action: X Button Pressed] - Canceled");
        // }
    }

    // Y Button Pressed
    public void yButtonPressed(InputAction.CallbackContext context)
    {
        // if (context.started)
        // {
        //     Debug.Log("[Action: Y Button Pressed] - Started");
        // }
        if (context.performed)
        {
            Debug.Log("[Action: Y Button Pressed] - Performed");
        }
        // if (context.canceled)
        // {
        //     Debug.Log("[Action: Y Button Pressed] - Canceled");
        // }
    }

    // A Button Pressed
    public void aButtonPressed(InputAction.CallbackContext context)
    {
        // if (context.started)
        // {
        //     Debug.Log("[Action: A Button Pressed] - Started");
        // }
        if (context.performed)
        {
            Debug.Log("[Action: A Button Pressed] - Performed");
        }
        // if (context.canceled)
        // {
        //     Debug.Log("[Action: A Button Pressed] - Canceled");
        // }
    }

    // B Button Pressed
    public void bButtonPressed(InputAction.CallbackContext context)
    {
        // if (context.started)
        // {
        //     Debug.Log("[Action: B Button Pressed] - Started");
        // }
        if (context.performed)
        {
            Debug.Log("[Action: B Button Pressed] - Performed");
        }
        // if (context.canceled)
        // {
        //     Debug.Log("[Action: B Button Pressed] - Canceled");
        // }
    }
}
