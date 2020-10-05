using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelectionDisplay : MonoBehaviour
{
    public Sprite _default = null;
    public List<Image> _images = new List<Image>(new Image[3]);

    private int[] selectInd = new int[3];

    // void Awake() {}
    // void Start() {}

    void Update()
    {
        if (BattleSystem._instance != null)
        {
            int count = 0;
            for (int i = 0; i < BattleSystem._instance._selected.Count; i++)
            {
                if (BattleSystem._instance._selected[i] == true)
                {
                    selectInd[count] = i;
                    count++;
                    count = Mathf.Clamp(count, 0, 3);
                }
            }
            if (count > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i < count)
                        _images[i].sprite = BattleSystem._instance._selectablePartners[selectInd[i]].CreatureImage;
                    else
                        _images[i].sprite = _default;
                }
            }
            else if (count == 0)
            {
                for (int i = 0; i < _images.Count; i++)
                    _images[i].sprite = _default;
            }
        }
    }

    // void FixedUpdate() {}
}
