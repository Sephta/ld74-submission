using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureColliderBehavior : MonoBehaviour
{
    [Header("Tick Data")]
    [Range(0, 1)] public float _tickRate = 0f;
    [SerializeField, ReadOnly] private float _timer = 0f;


    void Start()
    {
        SetTimer();
    }

    // void OnCollisionEnter2D(Collision2D collision) {}
    void OnCollisionExit2D(Collision2D collision)
    {
        SetTimer();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        DecramentTimer();

        if (_timer == 0)
        {
            if (collision.gameObject.tag == "Creature")
            {
                Debug.Log("<" + gameObject.name + "> hit <" + collision.gameObject.name + "> for " + gameObject.GetComponent<CreatureBattleAI>()._dmgStat + " dmg");
                collision.gameObject.GetComponent<CreatureBattleAI>().TakeDamage(gameObject.GetComponent<CreatureBattleAI>()._dmgStat);
            }
            SetTimer();
        }
    }

    private void SetTimer() { _timer = _tickRate; }
    private void DecramentTimer()
    {
        _timer -= Time.deltaTime;
        _timer = Mathf.Clamp(_timer, 0, _tickRate);
    }
}
