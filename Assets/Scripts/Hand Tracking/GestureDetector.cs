using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GestureDetector : MonoBehaviour
{
    [SerializeField] private GameObject startText;

    public void StartTask(){
        Debug.Log("start task called");
        StartCoroutine(StartingTask());
    }
    IEnumerator StartingTask(){
        startText.SetActive(true);
        yield return new WaitForSeconds(1f);
        Debug.Log("User starting task");
        startText.SetActive(false);
    }
}
