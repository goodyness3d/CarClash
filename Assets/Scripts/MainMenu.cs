using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MasterScript gameMaster;

    [SerializeField] private CinemachineVirtualCamera[] UIcams;

    [SerializeField] private Animator mainCanvasAnimator;


    private void Start()
    {
        gameMaster.LoadGame();
    }


    private void SetUIcamPriority(int camIndex, bool add)
    {
        if (UIcams.Length > camIndex)
        {
            if (add)
            {
                UIcams[camIndex].Priority = 1;
            }

            else
            {
                UIcams[camIndex].Priority = 0;
            }
        }
    }


    public void OpenPlayMenu()
    {
        mainCanvasAnimator.SetInteger("menuToOpen", 1);
        SetUIcamPriority(1, true);
    }

    public void ClosePlayMenu()
    {
        mainCanvasAnimator.SetInteger("menuToOpen", 0);
        SetUIcamPriority(1, false);
    }

    public void OpenCampaign()
    {
        mainCanvasAnimator.SetInteger("menuToOpen", 2);
        SetUIcamPriority(2, true);
    }

    public void LeaveCampaign()
    {
        mainCanvasAnimator.SetInteger("menuToOpen", 1);
        SetUIcamPriority(2, false);
    }

    public void OpenMultiplayer()
    {
        mainCanvasAnimator.SetInteger("menuToOpen", 3);
        SetUIcamPriority(3, true);
    }

    public void LeaveMultiplayer()
    {
        mainCanvasAnimator.SetInteger("menuToOpen", 1);
        SetUIcamPriority(3, false);
    }

    public void LoadLevel(int increment)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + increment);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
