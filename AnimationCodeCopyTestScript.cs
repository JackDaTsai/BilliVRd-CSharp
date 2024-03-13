using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AnimationCodeCopyTestScript : MonoBehaviour
{
    public GameObject[] Body;
    private List<string> animationData;
    private int counter = 0;
    private Vector3[] targetPositions; // 保存目標位置
    private float interpolationFactor = 0.5f; // 插值因子，控制平滑程度
    public int MotionFileNumber = 0;

    void Start()
    {
        if (MotionFileNumber != 0)
        {
            animationData = new List<string>();
            // 使用 StreamReader 逐行讀取文本檔案
            // @"C:\Users\admin\Desktop\Python\MotionFileFolder\MotionFile" + motionFileNumber + ".txt"
            if (File.Exists("C:/Users/admin/Desktop/Python/MotionFileFolder/MotionFile" + MotionFileNumber + ".txt"))
            {
                using (StreamReader reader = new StreamReader("C:/Users/admin/Desktop/Python/MotionFileFolder/MotionFile" + MotionFileNumber + ".txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        animationData.Add(line);
                    }
                }
                targetPositions = new Vector3[Body.Length]; // 初始化目標位置陣列
            }
            else
            {
                using (StreamReader reader = new StreamReader("Assets/AnimationFolder/AnimationFile4.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        animationData.Add(line);
                    }
                }
                targetPositions = new Vector3[Body.Length]; // 初始化目標位置陣列
            }
        }
    }

    void Update()
    {
        if (counter < animationData.Count)
        {
            string receivedData = animationData[counter];
            string[] points = receivedData.Split(',');
            if (points.Length >= 99) // 檢查陣列長度是否足夠大
            {
                for (int i = 0; i < 33 && i < Body.Length; i++)
                {
                    float x, y, z;
                    if (float.TryParse(points[0 + (i * 3)], out x) && float.TryParse(points[1 + (i * 3)], out y) && float.TryParse(points[2 + (i * 3)], out z))
                    {
                        x /= 100f;
                        y /= 100f;
                        z /= 300f;
                        Vector3 targetPosition = new Vector3(x-3, y-1, z);
                        targetPositions[i] = targetPosition; // 保存目標位置
                    }
                    else
                    {
                        //print("Failed to parse float values for index \n");
                        //Debug.LogError("Failed to parse float values for index " + i);
                    }
                }
            }
            else
            {
                print("站遠一點\n");
                Debug.LogError("Received data does not have enough points");
            }

            // 插值平滑過渡
            for (int i = 0; i < Body.Length; i++)
            {
                Vector3 currentPosition = Body[i].transform.localPosition;
                Vector3 targetPosition = targetPositions[i];
                Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, interpolationFactor);
                Body[i].transform.localPosition = newPosition;
            }

            counter++;
            if (counter == animationData.Count)
                counter = 0;
        }

        StartCoroutine(WaitForNextFrame());//等待一段時間
    }

    IEnumerator WaitForNextFrame()
    {
        yield return new WaitForSeconds(1); // 等待 1 秒
    }
}
    