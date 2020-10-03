using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureDataAsset", menuName = "ScriptableObjects/CreatureData Asset", order = 0)]
public class CreatureData : ScriptableObject
{
    // PRIVATE DATA
    [SerializeField] private string _creatureName = "";
    [SerializeField] private string _displayName = "";
    [SerializeField] private int _creatureUniqueID = 0;
    [SerializeField] private Sprite _creatureImage = null;
    [SerializeField] private int _creatureEXP = 0;
    [SerializeField] private Vector2 _creatureColliderSize = Vector2.zero;
    [SerializeField] private List<int> _creatureStats = new List<int>();

    // PUBLIC DATA
    public string CreatureName { get { return _creatureName; } }
    public string DisplayName { get { return _displayName; } }
    public int CreatureID { get { return _creatureUniqueID; } }
    public Sprite CreatureImage { get { return _creatureImage; } }
    public int CreatureEXP { get { return _creatureEXP; } }
    public Vector2 ColliderSize { get { return _creatureColliderSize; } }
    public List<int> CreatureStats { get { return _creatureStats; } }
}
