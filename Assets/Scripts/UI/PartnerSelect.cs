using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartnerSelect : MonoBehaviour
{
    public Toggle _toggle = null;

    // void Awake() {}

    void Start()
    {
        if (GetComponent<Toggle>() != null)
            _toggle = GetComponent<Toggle>();
    }

    // void Update() {}
    // void FixedUpdate() {}

    public void SelectPartner(int index)
    {
        if (BattleSystem._instance != null && _toggle != null)
        {
            if (_toggle.isOn == false)
            {
                BattleSystem._instance._selected[index] = false;
                return;
            }

            int count = 0;
            foreach (bool selection in BattleSystem._instance._selected)
            {
                if (selection == true)
                    count++;
            }

            if (count == 3)
                _toggle.isOn = false;
            else
                BattleSystem._instance._selected[index] = _toggle.isOn;
        }
    }
}
