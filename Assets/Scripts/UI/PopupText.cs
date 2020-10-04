using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    // void Awake() {}
    // void Start() {}
    // void Update() {}
    // void FixedUpdate() {}

    public void DestroyParent()
    {
        GameObject _p = gameObject.transform.parent.gameObject;

        Destroy(_p);
    }
}
