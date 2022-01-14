using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Text taptostart;
    public GameObject meter;
    public GameObject pause;
    public GameObject resume;
    public GameObject restart;

    void Start()
    {
        if (!Input.touchSupported)
        {
            taptostart.text = "Space to Start";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SplashScreen.isFinished && (Input.GetKey(KeyCode.Space) || Input.touchSupported && Input.touchCount > 0))
        {
            taptostart.gameObject.SetActive(false);
            meter.SetActive(true);
            pause.SetActive(true);
            // if (!EventSystem.current.IsPointerOverGameObject(0))
            //     Time.timeScale = 1;
        }

        if (Input.GetKeyDown((KeyCode.Escape)))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        resume.SetActive(true);
        restart.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        resume.SetActive(false);
        restart.SetActive(false);
    }

    public void Reload()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}