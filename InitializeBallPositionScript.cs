using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeBallPositionScript : MonoBehaviour
{
    /* Reference to White Ball */
    public GameObject WhiteBall_GameObject;
    /* Reference to Black Ball */
    public GameObject BlackBall_GameObject;
    /* Reference to Blue Ball */
    public GameObject BlueBall_GameObject;

    // Start is called before the first frame update
    void Start()
    {
        // 等待 2 秒再執行
        StartCoroutine(InitializeBallPosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InitializeBallPosition()
    {
        // 等待 2 秒
        yield return new WaitForSeconds(2);

        // Generate random position X
        float randomPositionX = Mathf.Round(Random.Range(-0.5f, 1f) * 10) / 10.0f;
        // Generate random position Z
        float randomPositionZ = Mathf.Round(Random.Range(0f, 1f) * 10) / 10.0f;
        WhiteBall_GameObject.GetComponent<Transform>().position = new Vector3(randomPositionX, 1f, randomPositionZ);
        //Debug.Log("ballGameObject.GetComponent<Transform>().position: " + WhiteBall_GameObject.GetComponent<Transform>().position);

        // Generate random position X
        randomPositionX = Mathf.Round(Random.Range(-0.5f, 1f) * 10) / 10.0f;
        // Generate random position Z
        randomPositionZ = Mathf.Round(Random.Range(0f, 1f) * 10) / 10.0f;
        BlackBall_GameObject.GetComponent<Transform>().position = new Vector3(randomPositionX, 1f, randomPositionZ);
        //Debug.Log("ballGameObject.GetComponent<Transform>().position: " + BlackBall_GameObject.GetComponent<Transform>().position);

        // Generate random position X
        randomPositionX = Mathf.Round(Random.Range(-0.5f, 1f) * 10) / 10.0f;
        // Generate random position Z
        randomPositionZ = Mathf.Round(Random.Range(0f, 1f) * 10) / 10.0f;
        BlueBall_GameObject.GetComponent<Transform>().position = new Vector3(randomPositionX, 1f, randomPositionZ);
        //Debug.Log("ballGameObject.GetComponent<Transform>().position: " + BlueBall_GameObject.GetComponent<Transform>().position);
    }
}