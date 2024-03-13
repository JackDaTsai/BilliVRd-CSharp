using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DTWwoSocketTestScript : MonoBehaviour
{
    public GameObject dtwResultTextGameObject;
    public GameObject gameObjectManager;
    public GameObject HumanMotionReplayGameObject;
    public GameObject Toggle1stGameObject;
    public GameObject Toggle2ndGameObject;
    public GameObject mixamorigHips;
    private Toggle _toggle1st;
    private Toggle _toggle2nd;

    // Start is called before the first frame update
    void Start()
    {
        gameObjectManager.GetComponent<AnimationCodeCopyTestScript>().enabled = false;
        // HumanMotionReplayGameObject.SetActive(false);
        dtwResultTextGameObject.SetActive(false);

        _toggle1st = Toggle1stGameObject.GetComponent<Toggle>();
        _toggle2nd = Toggle2ndGameObject.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Dynamic Bool: On Value Changed
    public void OnValueChanged_1stMotionAnalysis(bool toggleIsOn)
    {
        if (toggleIsOn)
        {
            _toggle2nd.isOn = false;
            gameObjectManager.GetComponent<AnimationCodeCopyTestScript>().enabled = true;
            gameObjectManager.GetComponent<AnimationCodeCopyTestScript>().MotionFileNumber = 1;            
            HumanMotionReplayGameObject.SetActive(true);
            mixamorigHips.GetComponent<PersonControlCopyScript>().enabled = true;

            string dtwScorePath = @"C:\Users\admin\Desktop\Python\dtw\DTW-Score\dtw_score1.txt";
            if (File.Exists(dtwScorePath))
            {
                string dtwScore = File.ReadAllText(dtwScorePath);
                DynamicTimeWarpingScore(dtwScore.Trim());
            }
            else
            {
                Debug.LogError("DTW Score file not found.");
            }
        }
        else
        {
            gameObjectManager.GetComponent<AnimationCodeCopyTestScript>().enabled = false;
            gameObjectManager.GetComponent<AnimationCodeCopyTestScript>().MotionFileNumber = 0;
            HumanMotionReplayGameObject.SetActive(false);
            mixamorigHips.GetComponent<PersonControlCopyScript>().enabled = false;
            dtwResultTextGameObject.SetActive(false);
        }
    }

    public void OnValueChanged_2ndMotionAnalysis(bool toggleIsOn)
    {
        if (toggleIsOn)
        {
            _toggle1st.isOn = false;
            gameObjectManager.GetComponent<AnimationCodeCopyTestScript>().enabled = true;
            gameObjectManager.GetComponent<AnimationCodeCopyTestScript>().MotionFileNumber = 2;
            HumanMotionReplayGameObject.SetActive(true);
            mixamorigHips.GetComponent<PersonControlCopyScript>().enabled = true;

            string dtwScorePath = @"C:\Users\admin\Desktop\Python\dtw\DTW-Score\dtw_score2.txt";
            if (File.Exists(dtwScorePath))
            {
                string dtwScore = File.ReadAllText(dtwScorePath);
                DynamicTimeWarpingScore(dtwScore.Trim());
            }
            else
            {
                Debug.LogError("DTW Score file not found.");
            }
        }
        else
        {
            gameObjectManager.GetComponent<AnimationCodeCopyTestScript>().enabled = false;
            gameObjectManager.GetComponent<AnimationCodeCopyTestScript>().MotionFileNumber = 0;
            HumanMotionReplayGameObject.SetActive(false);
            mixamorigHips.GetComponent<PersonControlCopyScript>().enabled = false;
            dtwResultTextGameObject.SetActive(false);
        }
    }

    public void DynamicTimeWarpingScore(string dtwScore)
    {
        dtwResultTextGameObject.SetActive(true);
        dtwResultTextGameObject.GetComponent<UnityEngine.UI.Text>().text = "DTW Distance: " + dtwScore;
    }
}
