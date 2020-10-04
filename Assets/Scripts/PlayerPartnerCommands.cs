using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartnerCommands : MonoBehaviour
{
    public GameObject _waypointRefr = null;

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

        if (Input.GetMouseButtonDown(0) && _selectedPartner != null)
        {
            if (Vector3.Distance(GetMouseWorldPos(), _selectedPartner.transform.position) > 0.5f)
            {
                GameObject refr = Instantiate(_waypointRefr, GetMouseWorldPos(), Quaternion.identity);
                _selectedPartner.GetComponent<CreatureBattleAI>()._currTarget = refr;
                refr.GetComponent<WaypointHandler>()._traveler = _selectedPartner;

                GameObject ringRefr = Instantiate(_selectedPartner.GetComponent<CreatureBattleAI>().ringParticle, GetMouseWorldPos(), Quaternion.identity);
                ParticleFunctions _ringData = ringRefr.GetComponent<ParticleFunctions>();
                ParticleSystem.MainModule _main = _ringData._system.main;
                _main.simulationSpeed = 2f;

                _selectedPartner = null;
            }
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 getMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(getMouse.x, getMouse.y, 0f);
    }
}
