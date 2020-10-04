using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [Header("References + Dependencies")]
    public GameObject _creatureRef = null;

    [Header("Battle System Data")]
    public List<GameObject> _playerCreatures = new List<GameObject>();
    public List<GameObject> _enemyCreatures = new List<GameObject>();

    public List<CreatureData> _selectablePartners = new List<CreatureData>(new CreatureData[8]);
    public List<bool> _selected = new List<bool>(new bool[8]);

    [Header("Instance Data")]
    public static BattleSystem _instance;
    [SerializeField, ReadOnly] private bool _gameStart = false;


    void Awake()
    {
        if (!_gameStart)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
            _gameStart = true;
        }
    }

    void Start()
    {
        // Remove this later...
        if (GameTimer._instance != null)
            GameTimer._instance.StartTimer();
    }

    void Update()
    {
        if (GameObject.Find("Creature Container") != null)
            _creatureRef = GameObject.Find("Creature Container");
    }

    // void FixedUpdate() {}
}
