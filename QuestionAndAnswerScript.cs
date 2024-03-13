using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class QuestionAndAnswerScript : MonoBehaviour
{
    // File Paths
    string QuestionFilePath = @"Assets\QandA\Questions.txt";
    string AnswerFilePath = @"Assets\QandA\Answers.txt";

    // Array of Questions and Answers
    string[] questions = null;
    string[] answers = null;

    // Index of the Current Question and Answer
    int currentQuestionIndex = 0;
    int currentAnswerIndex = 0;
    int initialIndex = 0;

    // Total Number of Correct Questions
    int totalCorrectQuestions = 0;

    // Reference to the Question and Answer Labels
    public GameObject[] questionLabels;
    public GameObject[] answerLabels;

    // Reference to the Total Number of Correct Questions Label
    public GameObject totalCorrectQuestionsLabel;
    public GameObject correctQuestionsNumberLabel;
    public GameObject questionLabel;

    // Start is called before the first frame update
    void Start()
    {
        ReadQuestionsFromFile(QuestionFilePath, ref questions, questionLabels);
        ReadAnswersFromFile(AnswerFilePath, ref answers);

        SetActiveLabel(questionLabels, initialIndex);
        SetActiveLabel(answerLabels, initialIndex);
    }

    void ReadQuestionsFromFile(string filePath, ref string[] lines, GameObject[] labels)
    {
        if (File.Exists(filePath))
        {
            lines = File.ReadAllLines(filePath);
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
            return;
        }


        for (int i = 0; i < lines.Length; i++)
        {
            labels[i].GetComponent<Text>().text = (i+1).ToString() +  ". " + lines[i];
        }
    }

    void ReadAnswersFromFile(string filePath, ref string[] lines)
    {
        if (File.Exists(filePath))
        {
            lines = File.ReadAllLines(filePath);
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
            return;
        }
    }

    // Dynamic Bool: On Value Changed
    public void OnValueChanged(bool toggleIsOn)
    {
        // Handle Question Labels
        questionLabels[currentQuestionIndex].SetActive(false);
        currentQuestionIndex++;
        if (currentQuestionIndex < questionLabels.Length)
        {
            questionLabels[currentQuestionIndex].SetActive(true);
        }
        else
        {
            // Reset the currentQuestionIndex
            currentQuestionIndex = 0;
            totalCorrectQuestionsLabel.SetActive(true);
            questionLabel.SetActive(false);
        }

        // Handle Answer Labels
        string[] gameObjectNames = answerLabels[currentAnswerIndex].GetComponentsInChildren<Toggle>().Select(t => t.name).ToArray();
        foreach (string gameObjectName in gameObjectNames)
        {
            if (answerLabels[currentAnswerIndex].transform.Find(gameObjectName).GetComponent<Toggle>().isOn)
            {
                // Debug.Log(gameObjectName);
                // if ((gameObjectName == "Icon Toggle O" ? true : false) == (answers[currentAnswerIndex] == "O" ? true : false))
                if ((gameObjectName == "O Text Toggle" ? true : false) == (answers[currentAnswerIndex] == "O" ? true : false))
                {
                    totalCorrectQuestions++;
                }
            }
        }

        answerLabels[currentAnswerIndex].SetActive(false);
        currentAnswerIndex++;
        if (currentAnswerIndex < answerLabels.Length)
        {
            answerLabels[currentAnswerIndex].SetActive(true);
        }
        else
        {
            // Reset the currentAnswerIndex
            currentAnswerIndex = 0;
            correctQuestionsNumberLabel.GetComponent<UnityEngine.UI.Text>().text = totalCorrectQuestions.ToString();
            correctQuestionsNumberLabel.SetActive(true);
        }
    }

    void SetActiveLabel(GameObject[] labels, int initialIndex)
    {
        //Debug.Log("Start SetActiveLabel");

        labels[initialIndex].SetActive(true);

        for (int i = initialIndex+1; i < labels.Length; i++)
        {
            labels[i].SetActive(false);
        }

        //Debug.Log("End SetActiveLabel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
