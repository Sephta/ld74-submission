using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDirector : MonoBehaviour
{
    [Header("Battle Data")]
    public BattleSystem _bs = null;
    public bool _battleStart = false;
    public List<GameObject> _entitySpawnLocals_player = new List<GameObject>(new GameObject[3]);
    public List<GameObject> _entitySpawnLocals_enemy = new List<GameObject>(new GameObject[3]);
    public int _enemySpawnAmount = 0;

    [Header("Instance Data")]
    public static BattleDirector _instance = null;
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
            _battleStart = false;
        }
    }

    // void FixedUpdate() {}

    private void SetupPlayerCreatureData(GameObject creature, int index)
    {
        CreatureBattleAI _bai = creature.GetComponent<CreatureBattleAI>();
        _bai._entityType = CreatureBattleAI.EntityType.player;
        
        CreatureData _cData = _bs._selectablePartners[index];

        // Data
        _bai._maxHealth = 100f;
        _bai._moveSpeed = 5f;
        _bai._dmgStat = 1f;
        _bai._knockback = 100f;
        _bai._spdStat = 0.5f;


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
        _bai._maxHealth = 100f;
        _bai._moveSpeed = 5f;
        _bai._dmgStat = 1f;
        _bai._knockback = 100f;
        _bai._spdStat = 0.5f;


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
}
