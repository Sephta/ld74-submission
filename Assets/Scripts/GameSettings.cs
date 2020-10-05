using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Header("Game Settings")]
    [Range(0, 1)] public float _sfxVolume = 0f;
    [Range(0, 1)] public float _musicVolume = 0f;

    [Header("Instance Data")]
    public static GameSettings _instance;
    private bool gameStart = false;

    void Awake()
    {
        if (!gameStart)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
            gameStart = true;
        }
    }

    // void Start() {}
    // void Update() {}
    // void FixedUpdate() {}
}
