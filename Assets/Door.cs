using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private ProbePoints probePoints;
    [SerializeField] private ContinueButton continueButton;
    [SerializeField] private string scene;

    public void Resume()
    {
        GameManager.Instance.nextScene = scene;
        GameManager.Instance.ResumeMove();
        gameObject.SetActive(false);
    }
    
    void Win()
    {
        continueButton.Enable();
    }
    void CheckWin()
    {
        if (probePoints.CheckWin())
            Win();
    }
    void Update()
    {
        CheckWin();
    }
}
