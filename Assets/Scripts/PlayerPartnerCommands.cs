using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartnerCommands : MonoBehaviour
{
    [ReadOnly] public GameObject _selectedPartner = null;
    [ReadOnly] public GameObject _selectedEnemy = null;

    public static PlayerPartnerCommands _instance;
    [SerializeField, ReadOnly] private bool _gameStart = false;


    void Awake()
    {
        if (!_gameStart)
        {
            _instance = this;
            _gameStart = true;
        }
    }

    void Update()
    {
        if (_selectedPartner != null && _selectedEnemy != null)
        {
            _selectedPartner.GetComponent<CreatureBattleAI>()._currTarget = _selectedEnemy;

            _selectedPartner = null;
            _selectedEnemy = null;
        }
    }
}
