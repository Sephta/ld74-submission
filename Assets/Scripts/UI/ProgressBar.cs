using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Text _name = null;
    public Image _mask = null;
    public float _max = 0;
    [ReadOnly] public float _current = 0;

    public bool _player = false;
    public bool _ability = false;
    public int index = 0;

    // void Awake() {}
    // void Start() {}

    void Update()
    {
        if (BattleSystem._instance != null)
        {
            if (_player)
            {
                if (index < BattleSystem._instance._playerCreatures.Count && BattleSystem._instance._playerCreatures[index] != null)
                {
                    _current = BattleSystem._instance._playerCreatures[index].GetComponent<CreatureBattleAI>()._currHealth;
                    _max = BattleSystem._instance._playerCreatures[index].GetComponent<CreatureBattleAI>()._maxHealth;
                }
                else
                    _current = 0;
            }
            else if (!_player && !_ability)
            {
                if (index < BattleSystem._instance._enemyCreatures.Count && BattleSystem._instance._enemyCreatures[index] != null)
                {
                    _current = BattleSystem._instance._enemyCreatures[index].GetComponent<CreatureBattleAI>()._currHealth;
                    _max = BattleSystem._instance._enemyCreatures[index].GetComponent<CreatureBattleAI>()._maxHealth;
                }
                else
                    _current = 0;
            }

        }

        GetCurrentFill();
    }

    // void FixedUpdate() {}

    public void GetCurrentFill()
    {
        _mask.fillAmount = (_current / _max);
    }
}
