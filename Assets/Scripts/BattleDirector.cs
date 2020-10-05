using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleDirector : MonoBehaviour
{
    [Header("Battle Data")]
    public BattleSystem _bs = null;
    public List<ProgressBar> _playerBars = new List<ProgressBar>();
    public List<ProgressBar> _enemyBars = new List<ProgressBar>();
    public bool _battleStart = false;
    public List<GameObject> _entitySpawnLocals_player = new List<GameObject>(new GameObject[3]);
    public List<GameObject> _entitySpawnLocals_enemy = new List<GameObject>(new GameObject[3]);
    public int _enemySpawnAmount = 0;

    public GameObject _gameOverScreen = null;

    [Header("Instance Data")]
    public static BattleDirector _instance = null;
    [SerializeField, ReadOnly] private bool _gameStart = false;
    [SerializeField, ReadOnly] private bool _gameOver = false;


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
        if (BattleSystem._instance != null)
            _bs = BattleSystem._instance;
    }
    
    void Update()
    {
        if (_battleStart)
        {
            if (GameTimer._instance != null)
                GameTimer._instance._begin = true;
            PlayerSetup();
            EnemySetup();

            for (int i = 0; i < _playerBars.Count; i++)
            {
                if (BattleSystem._instance._playerCreatures[i] != null)
                    _playerBars[i]._max = BattleSystem._instance._playerCreatures[i].GetComponent<CreatureBattleAI>()._maxHealth;
            }
            for (int i = 0; i < _enemyBars.Count; i++)
            {
                if (BattleSystem._instance._enemyCreatures[i] != null)
                    _enemyBars[i]._max = BattleSystem._instance._enemyCreatures[i].GetComponent<CreatureBattleAI>()._maxHealth;
            }

            if (GameObject.Find("Creature Container") != null)
                Destroy(GameObject.Find("Creature Container"));
            _battleStart = false;
        }
        else if (!_gameOver)
        {
            if (GameTimer._instance._timeLeft == 0)
                GameOver("Game Over");
            else if (BattleSystem._instance != null && BattleSystem._instance._playerCreatures.Count <= 0)
                GameOver("Game Over");
            else if (BattleSystem._instance != null && BattleSystem._instance._enemyCreatures.Count <= 0)
                GameOver("You Win!");

            if (BattleSystem._instance != null)
            {
                for (int i = 0; i < BattleSystem._instance._playerCreatures.Count; i++)
                {
                    if (BattleSystem._instance._playerCreatures[i] == null)
                        BattleSystem._instance._playerCreatures.Remove(BattleSystem._instance._playerCreatures[i]);
                }
                for (int i = 0; i < BattleSystem._instance._enemyCreatures.Count; i++)
                {
                    if (BattleSystem._instance._enemyCreatures[i] == null)
                        BattleSystem._instance._enemyCreatures.Remove(BattleSystem._instance._enemyCreatures[i]);
                }
            }
        }
    }

    // void FixedUpdate() {}

    private void SetupPlayerCreatureData(GameObject creature, int index)
    {
        CreatureBattleAI _bai = creature.GetComponent<CreatureBattleAI>();
        _bai._entityType = CreatureBattleAI.EntityType.player;
        
        CreatureData _cData = _bs._selectablePartners[index];

        // Data
        _bai._maxHealth = (float)_cData.CreatureStats[0];
        _bai._dmgStat = (float)_cData.CreatureStats[1];
        _bai._knockback = (float)_cData.CreatureStats[2];
        _bai._moveSpeed = (float)_cData.CreatureStats[3];
        _bai._spdStat = ((float)_cData.CreatureStats[4]) / 100;

        // float test1 = Random.Range(200f, 500f);
        // float test2 = Random.Range(0.25f, 0.8f);

        SpriteRenderer cSprite = creature.GetComponent<SpriteRenderer>();
        cSprite.sprite = _cData.CreatureImage;

        creature.name = _cData.CreatureName + " (P)";
    }

    private void PlayerSetup()
    {
        if (_bs == null || _bs._creatureRef == null)
        {
            Debug.Log("Warning<" + gameObject.name + ">. reference to BattleSystem is null.");
            return;   
        }
        
        int spawnCount = 0;
        for (int i = 0; i < _bs._selected.Count; i++)
        {
            if (_bs._selected[i])
            {
                GameObject refr = Instantiate(_bs._creatureRef, Vector3.zero, Quaternion.identity);
                Transform child = refr.transform.GetChild(0);
                SetupPlayerCreatureData(child.gameObject, i);
                child.position = _entitySpawnLocals_player[spawnCount].transform.position;
                Debug.Log("Spawned at: " + _entitySpawnLocals_player[spawnCount].name);
                spawnCount++;
                refr.name = "Player - " + spawnCount.ToString();

                _bs._playerCreatures.Add(child.gameObject);
            }
        }
    }

    private void SetupEnemyCreatureData(GameObject creature, int index)
    {
        CreatureBattleAI _bai = creature.GetComponent<CreatureBattleAI>();
        _bai._entityType = CreatureBattleAI.EntityType.enemy;

        CreatureData _cData = _bs._selectablePartners[index];

        // Data
        float stat1 = Random.Range(100f, 151f);
        _bai._maxHealth = stat1;
        float stat2 = Random.Range(4f, 9f);
        _bai._moveSpeed = 5f;
        float stat3 = Random.Range(10f, 16f);
        _bai._dmgStat = stat3;
        float stat4 = Random.Range(200f, 351f);
        _bai._knockback = stat4;
        float stat5 = Random.Range(0.25f, 0.81f);
        _bai._spdStat = stat5;


        SpriteRenderer cSprite = creature.GetComponent<SpriteRenderer>();
        cSprite.sprite = _cData.CreatureImage;

        creature.name = _cData.CreatureName + " (E)";
    }

    private void EnemySetup()
    {
        if (_bs == null || _bs._creatureRef == null)
            return;

        int  spawnCount = 0;
        for (int i = 0; i < _enemySpawnAmount; i++)
        {
            int spawnindex = (int)Random.Range(0, 8);
            GameObject refr = Instantiate(_bs._creatureRef, Vector3.zero, Quaternion.identity);
            Transform child = refr.transform.GetChild(0);
            SetupEnemyCreatureData(child.gameObject, spawnindex);
            child.position = _entitySpawnLocals_enemy[spawnCount].transform.position;
            spawnCount++;
            refr.name = "Enemy - " + spawnCount.ToString();
            _bs._enemyCreatures.Add(child.gameObject);
        }
    }

    private void GameOver(string gameOverText)
    {
        _gameOver = true;

        GameObject refr = Instantiate(_gameOverScreen, GameObject.Find("UI - Overlay").transform);
        refr.transform.GetChild(0).gameObject.GetComponent<Text>().text = gameOverText;

        for (int i = 0; i < BattleSystem._instance._playerCreatures.Count; i++)
        {
            BattleSystem._instance._playerCreatures.Remove(BattleSystem._instance._playerCreatures[i]);
        }
        for (int i = 0; i < BattleSystem._instance._enemyCreatures.Count; i++)
        {
            BattleSystem._instance._enemyCreatures.Remove(BattleSystem._instance._enemyCreatures[i]);
        }

        for (int i = 0; i < BattleSystem._instance._selected.Count; i++)
            BattleSystem._instance._selected[i] = false;
    }
}
