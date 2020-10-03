using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [Header("Battle System Data")]
    public List<GameObject> _playerCreatures = new List<GameObject>();
    public List<GameObject> _enemyCreatures = new List<GameObject>();

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
        // Remove this later...
        GameTimer._instance.StartTimer();
    }

    // void Update() {}
    // void FixedUpdate() {}
}
