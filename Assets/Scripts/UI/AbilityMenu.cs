using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMenu : MonoBehaviour
{
    // void Awake() {}
    // void Start() {}
    // void Update() {}
    // void FixedUpdate() {}

    public void ActivateAbility(int index)
    {
        if (BattleSystem._instance != null)
        {
            CreatureBattleAI _bai = BattleSystem._instance._playerCreatures[index].GetComponent<CreatureBattleAI>();
            _bai._abilityTrigger = true;
        }
    }
}
