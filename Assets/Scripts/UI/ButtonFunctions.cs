using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // void Start() {}
    // void Update() {}
    // void FixedUpdate() {}

    public void BeginBattle()
    {
        if (BattleSystem._instance != null)
        {
            int count = 0;
            foreach (bool selection in BattleSystem._instance._selected)
                if (selection == true)
                    count++;

            if (count < 3 || count > 3)
                return;

            SceneManager.LoadScene("Battle");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log(scene.name + " " + mode);
        if (scene.name == "Battle")
            if (BattleDirector._instance != null)
            {
                BattleDirector._instance._battleStart = true;
            }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("BattleSetup");
    }

    public void ToggleOptionsMenu()
    {
        // TODO - do
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
