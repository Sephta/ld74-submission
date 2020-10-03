﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CreatureBattleAI))]
public class CreatureColliderBehavior : MonoBehaviour
{
    [Header("Dependencies")]
    public CreatureBattleAI _bai = null;

    [Header("Tick Data")]
    [Range(0, 1)] public float _tickRate = 0f;
    [SerializeField, ReadOnly] private float _timer = 0f;


    void Awake()
    {
        if (GetComponent<CreatureBattleAI>() != null)
            _bai = GetComponent<CreatureBattleAI>();
    }

    // void Start() {}

    // void OnCollisionEnter2D(Collision2D collision) {}
    void OnCollisionExit2D(Collision2D collision)
    {
        // SetTimer(0f);
        // _bai.SetTimer(0f);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // DecramentTimer();
        _bai.DecramentTimer();

        if (_bai._atkTimer == 0)
        {
            if (collision.gameObject.tag == "Creature")
            {
                Debug.Log("<" + gameObject.name + "> hit <" + collision.gameObject.name + "> for " + gameObject.GetComponent<CreatureBattleAI>()._dmgStat + " dmg");
                collision.gameObject.GetComponent<CreatureBattleAI>().TakeDamage(_bai._dmgStat, _bai._knockback);
            }
            _bai.SetTimer(_bai._spdStat);
            // SetTimer(_tickRate);
        }
    }

    private void SetTimer(float setAmount) { _timer = setAmount; }
    private void DecramentTimer()
    {
        _timer -= Time.deltaTime;
        _timer = Mathf.Clamp(_timer, 0, _tickRate);
    }
}
