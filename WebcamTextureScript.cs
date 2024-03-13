using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamTextureScript : MonoBehaviour
{
    // Reference to the Plane object
    [SerializeField] private GameObject _plane;
    // Define a scale factor
    [SerializeField] private float _scaleFactor = 0.15f; // 15% of the original size

    // Start is called before the first frame update
    private void Start()
    {
        // Get the webcam
        WebCamDevice[] devices = WebCamTexture.devices;
        // Create a WebCamTexture object
        WebCamTexture webcamTexture = new WebCamTexture(devices[0].name);
        // Project the WebCamTexture onto the Plane object
        _plane.GetComponent<Renderer>().material.mainTexture = webcamTexture;
        // Solve Mirror Problem in 3D Space
        _plane.transform.localScale = new Vector3(-_scaleFactor, _scaleFactor, _scaleFactor);
        // Start the WebCamTexture
        webcamTexture.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        // No update logic for now
    }
}