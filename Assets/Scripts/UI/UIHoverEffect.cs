using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIHoverEffect : MonoBehaviour
{
    public UIStatMenuData _statData = null;
    public CreatureData _cData = null;

    // void Awake() {}
    // void Start() {}
    // void Update() {}
    // void FixedUpdate() {}

    void OnMouseOver()
    {
        // Debug.Log("Hovering over -> " + gameObject.name);
        if (_statData != null && _cData != null)
        {
            _statData.creatureImage.sprite = _cData.CreatureImage;
            _statData.creatureName.text = _cData.CreatureName;
            _statData.creatureDesc.text = _cData.CreatureDescription;

            foreach (Text stat in _statData.statVals)
                stat.text = _cData.CreatureStats[_statData.statVals.IndexOf(stat)].ToString();
        }
    }
}
