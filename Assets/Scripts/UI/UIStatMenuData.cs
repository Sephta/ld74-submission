using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIStatMenuData : MonoBehaviour
{
    [Header("Stat Objects")]
    public Image creatureImage = null;
    public Text creatureName = null;
    public Text creatureDesc = null;

    // public List<Image> statIcons = new List<Image>(new Image[6]);
    public List<Text> statVals = new List<Text>(new Text[6]);
}
