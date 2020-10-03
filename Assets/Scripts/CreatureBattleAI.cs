using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBattleAI : MonoBehaviour
{
    [Header("Dependencies")]
    Rigidbody2D _rb = null;

    [Header("Creature Battle Data")]
    public EntityType _entityType = EntityType.enemy;
    [ReadOnly] public GameObject _currTarget = null;
    public float _moveSpeed = 0f;
    
    // STATS -------------------------------------------------------------
    // damage stat
    public float _dmgStat = 0f;
    public float _knockback = 0f;
    // attack speed stat
    [Range(0, 1), Tooltip("Rate at which attacks land. (in seconds)")]
    public float _spdStat = 0f;
    [ReadOnly] public float _atkTimer = 0f;
    
    // Entity Health ---------------------------------
    [SerializeField] public float _maxHealth = 0f;
    [SerializeField, ReadOnly] public float _currHealth = 0f;
    // -----------------------------------------------
    // end STATS ---------------------------------------------------------

    [Header("Battle Data")]
    [SerializeField] public List<GameObject> _enemyCreatures = new List<GameObject>();

    public enum EntityType
    {
        nullEntity = 0,
        player = 1,
        enemy = 2
    }


    // void Awake() {}

    void Start()
    {
        if (GetComponent<Rigidbody2D>() != null)
            _rb = GetComponent<Rigidbody2D>();

        // Find initial closest enemy
        GetTargets();

        GameObject prev = this._enemyCreatures[0];
        foreach (GameObject enemy in  this._enemyCreatures)
                if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, prev.transform.position))
                    prev = enemy;
        
        _currTarget = prev;
        _currHealth = _maxHealth;
    }

    void Update()
    {
        // DecramentTimer();

        if (_currHealth == 0)
        {
            Debug.Log(gameObject.name + " has been defeated.");
            Destroy(gameObject);
        }

        // if (_atkTimer == 0)
        //     SetTimer(_spdStat);
    }

    void FixedUpdate()
    {
        // _rb.MovePosition(_currTarget.transform.position);
        if (_entityType != (EntityType)0 && _currTarget != null)
            _rb.AddForce(((CalculateDirection() * _moveSpeed) - _rb.velocity) * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }


    public void TakeDamage(float amount, float knockback)
    {
        _currHealth -= amount;
        _currHealth = Mathf.Clamp(_currHealth, 0, _maxHealth);
        ApplyKnockback(knockback);
    }

    public void ApplyKnockback(float amount)
    {
        float clampX = ((float)Random.Range(-100, 101)) / 100;
        float clampY = ((float)Random.Range(-100, 101)) / 100;
        clampX = Mathf.Clamp(clampX, -1f, 1f);
        clampY = Mathf.Clamp(clampY, -1f, 1f);
        Vector2 variationY = new Vector2(clampX, clampY);

        _rb.AddForce(((variationY * 100f) - _rb.velocity) * Time.fixedDeltaTime, ForceMode2D.Impulse);
        _rb.AddForce(((CalculateDirection() * -amount) - _rb.velocity) * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }


    private Vector2 CalculateDirection()
    {
        float moveX = _currTarget.transform.position.x - transform.position.x;
        float moveY = _currTarget.transform.position.y - transform.position.y;

        moveX = Mathf.Clamp(moveX, -1, 1);
        moveY = Mathf.Clamp(moveY, -1, 1);

        return new Vector2(moveX, moveY);
    }

    private void GetTargets()
    {

        // If this is the player entity
        if (_entityType == EntityType.player)
        {
            if (BattleSystem._instance != null)
            {
                foreach (GameObject enemy in BattleSystem._instance._enemyCreatures)
                    if (enemy != null)
                        this._enemyCreatures.Add(enemy);
            }
        }
        // If this is an enemy entity
        else
        {
            if (BattleSystem._instance != null)
            {
                foreach (GameObject enemy in BattleSystem._instance._playerCreatures)
                    if (enemy != null)
                        this._enemyCreatures.Add(enemy);
            }
        }
    }

    public void SetTimer(float amount) { _atkTimer = amount; }

    public void DecramentTimer()
    {
        _atkTimer -= Time.deltaTime;
        _atkTimer = Mathf.Clamp(_atkTimer, 0, _spdStat);
    }
}
