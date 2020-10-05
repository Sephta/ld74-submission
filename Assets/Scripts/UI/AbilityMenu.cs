using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class AbilityMenu : MonoBehaviour
{
    [Header("Progress Meters")]
    public ProgressBar _healBar = null;
    public ProgressBar _damageBar = null;
    public ProgressBar _speedBar = null;

    [Header("Cooldown Time")]
    public float _coolDownHeal = 0f;
    public float _coolDownDamage = 0f;
    public float _coolDownSpeed = 0f;

    [SerializeField, ReadOnly] private float _currHealTimer = 0f;
    [SerializeField, ReadOnly] private float _currDamageTimer = 0f;
    [SerializeField, ReadOnly] private float _currSpeedTimer = 0f;

    [Header("Abilty Stats")]
    public float _regenRate = 0f;
    [Range(0, 2)] public float _dmgMultiplier = 0f;
    [Range(0, 2)] public float _speedMultiplier = 0f;


    private bool _healFlag = false;
    private bool _damageFlag = false;
    private bool _speedFlag = false;

    // DMG
    private List<float> _oldDmg = new List<float>(new float[3]);
    private List<bool> _multiplierSet_dmg = new List<bool>(new bool[3]);

    // SPD
    private List<float> _oldSpd = new List<float>(new float[3]);
    private List<bool> _multiplierSet_spd = new List<bool>(new bool[3]);


    // void Awake() {}

    void Start()
    {
        _healBar._max = _healBar._current = _coolDownHeal;
        _damageBar._max = _damageBar._current = _coolDownDamage;
        _speedBar._max = _speedBar._current = _coolDownSpeed;

        _currHealTimer = _coolDownHeal;
        _currDamageTimer = _coolDownDamage;
        _currSpeedTimer = _coolDownSpeed;
    }

    void Update()
    {
        if (_healFlag)
        {
            _currHealTimer += Time.deltaTime;
            _currHealTimer = Mathf.Clamp(_currHealTimer, 0, _coolDownHeal);

            _healBar._current = _currHealTimer;

            for (int i = 0; i < BattleSystem._instance._playerCreatures.Count; i++)
            {
                if (BattleSystem._instance._playerCreatures[i] != null)
                {
                    CreatureBattleAI _bai = BattleSystem._instance._playerCreatures[i].GetComponent<CreatureBattleAI>();
                
                    _bai._currHealth += _regenRate * Time.deltaTime;
                    _bai._currHealth = Mathf.Clamp(_bai._currHealth, 0, _bai._maxHealth);
                }
            }

            if (_currHealTimer >= _coolDownHeal)
                _healFlag = false;
        }

        if (_damageFlag)
        {
            _currDamageTimer += Time.deltaTime;
            _currDamageTimer = Mathf.Clamp(_currDamageTimer, 0, _coolDownDamage);

            _damageBar._current = _currDamageTimer;

            for (int i = 0; i < BattleSystem._instance._playerCreatures.Count; i++)
            {
                if (!_multiplierSet_dmg[i] && BattleSystem._instance._playerCreatures[i] != null)
                {
                    CreatureBattleAI _bai = BattleSystem._instance._playerCreatures[i].GetComponent<CreatureBattleAI>();
                    _oldDmg[i] = _bai._dmgStat;
                    _bai._dmgStat *= _dmgMultiplier;
                    _multiplierSet_dmg[i] = true;
                }
            }

            if (_currDamageTimer >= _coolDownDamage)
            {
                for (int i = 0; i < _multiplierSet_dmg.Count; i++)
                {
                    CreatureBattleAI _bai = BattleSystem._instance._playerCreatures[i].GetComponent<CreatureBattleAI>();
                    _multiplierSet_dmg[i] = false;
                    _bai._dmgStat = _oldDmg[i];
                }
                _damageFlag = false;
            }
        }

        if (_speedFlag)
        {
            _currSpeedTimer += Time.deltaTime;
            _currSpeedTimer = Mathf.Clamp(_currSpeedTimer, 0, _coolDownSpeed);

            _speedBar._current = _currSpeedTimer;

            for (int i = 0; i < BattleSystem._instance._playerCreatures.Count; i++)
            {
                if (!_multiplierSet_spd[i] && BattleSystem._instance._playerCreatures[i] != null)
                {
                    CreatureBattleAI _bai = BattleSystem._instance._playerCreatures[i].GetComponent<CreatureBattleAI>();
                    _oldSpd[i] = _bai._spdStat;
                    _bai._spdStat /= _speedMultiplier;
                    _multiplierSet_spd[i] = true;
                }
            }

            if (_currSpeedTimer >= _coolDownSpeed)
            {
                for (int i = 0; i < _multiplierSet_dmg.Count; i++)
                {
                    CreatureBattleAI _bai = BattleSystem._instance._playerCreatures[i].GetComponent<CreatureBattleAI>();
                    _multiplierSet_spd[i] = false;
                    _bai._spdStat = _oldSpd[i];
                }

                _speedFlag = false;
            }
        }
    }

    // void FixedUpdate() {}

    public void ActivateAbility(int index)
    {
        if (BattleSystem._instance != null)
        {
            CreatureBattleAI _bai = BattleSystem._instance._playerCreatures[index].GetComponent<CreatureBattleAI>();
            _bai._abilityTrigger = true;
        }
    }

    public void HealAbility()
    {
        if (!(_currHealTimer >= _coolDownHeal))
        {
            if (AudioManager._instance != null)
                AudioManager._instance.PlaySFX(9);
            return;
        }

        if (AudioManager._instance != null)
                AudioManager._instance.PlaySFX(7);

        _currHealTimer = 0f;
        _healFlag = true;

        if (BattleSystem._instance == null || BattleSystem._instance._playerCreatures.Count <= 0)
            return;

        for (int i = 0; i < BattleSystem._instance._playerCreatures.Count; i++)
        {
            CreatureBattleAI _bai = BattleSystem._instance._playerCreatures[i].GetComponent<CreatureBattleAI>();
            GameObject refr = Instantiate(_bai.effectParticle, _bai.transform);
            refr.transform.position -= new Vector3(0f, 0.35f, 0f);
            ParticleSystem.MainModule main = refr.GetComponent<ParticleFunctions>()._system.main;
            Light2D light = refr.transform.GetChild(1).GetComponent<Light2D>();
            
            Color lightGreen = new Color((99f/255f), (199f/255f), (77f/255f), 1f);

            main.simulationSpeed = 5f;
            main.startColor = Color.green;
            light.color = Color.green;
        }
    }

    public void DamageAbility()
    {
        if (!(_currDamageTimer >= _coolDownDamage))
        {
            if (AudioManager._instance != null)
                AudioManager._instance.PlaySFX(9);
            return;
        }

        if (AudioManager._instance != null)
                AudioManager._instance.PlaySFX(7);

        _currDamageTimer = 0f;
        _damageFlag = true;

        if (BattleSystem._instance == null || BattleSystem._instance._playerCreatures.Count <= 0)
            return;
        
        for (int i = 0; i < BattleSystem._instance._playerCreatures.Count; i++)
        {
            CreatureBattleAI _bai = BattleSystem._instance._playerCreatures[i].GetComponent<CreatureBattleAI>();
            GameObject refr = Instantiate(_bai.effectParticle, _bai.transform);
            refr.transform.position -= new Vector3(0f, 0.35f, 0f);
            ParticleSystem.MainModule main = refr.GetComponent<ParticleFunctions>()._system.main;
            Light2D light = refr.transform.GetChild(1).GetComponent<Light2D>();

            Color redOrange = new Color((254f/255f), (231f/255f), (97f/255f), 1f);

            main.simulationSpeed = 5f;
            main.startColor = redOrange;
            light.color = redOrange;
        }
    }

    public void SpeedAbility()
    {
        if (!(_currSpeedTimer >= _coolDownSpeed))
        {
            if (AudioManager._instance != null)
                AudioManager._instance.PlaySFX(9);
            return;
        }

        if (AudioManager._instance != null)
                AudioManager._instance.PlaySFX(7);

        _currSpeedTimer = 0f;
        _speedFlag = true;

        if (BattleSystem._instance == null || BattleSystem._instance._playerCreatures.Count <= 0)
            return;

        for (int i = 0; i < BattleSystem._instance._playerCreatures.Count; i++)
        {
            CreatureBattleAI _bai = BattleSystem._instance._playerCreatures[i].GetComponent<CreatureBattleAI>();
            GameObject refr = Instantiate(_bai.effectParticle, _bai.transform);
            refr.transform.position -= new Vector3(0f, 0.35f, 0f);
            ParticleSystem.MainModule main = refr.GetComponent<ParticleFunctions>()._system.main;
            Light2D light = refr.transform.GetChild(1).GetComponent<Light2D>();

            Color lightBlue = new Color((44f/255f), (232f/255f), (245f/255f), 1f);

            main.simulationSpeed = 5f;
            main.startColor = lightBlue;
            light.color = lightBlue;
        }
    }
}
