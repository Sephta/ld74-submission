using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [Header("Battle System Data")]
    public List<bool> _playerCreatures = new List<bool>(new bool[3]);
    public List<bool> _enemyCreatures = new List<bool>(new bool[3]);

    [Header("Instance Data")]
    public static BattleSystem _instance;
    [SerializeField, ReadOnly] private bool _gameStart = false;


    void Awake()
    {
        if (!_gameStart)
        {
            _instance = this;
            _gameStart = true;
        }
    }

    void Start()
    {
        GameTimer._instance.StartTimer();
    }

    // void Update() {}
    // void FixedUpdate() {}
}
