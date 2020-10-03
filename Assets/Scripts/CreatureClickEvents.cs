using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureClickEvents : MonoBehaviour
{
    public bool _debug_MousePosition = false;
    [SerializeField, ReadOnly] public Vector2 _mousePos = Vector2.zero;

    // void Awake() {}
    // void Start() {}

    void Update()
    {
        if (_debug_MousePosition)
            _mousePos = GetMouseWorldPos();
    }

    // void FixedUpdate() {}

    // void OnMouseDown() {}
    // void OnMouseUp() {}
    // void OnMouseEnter() {}
    // void OnMouseExit() {}

    void OnMouseDrag()
    {
        if (Input.GetMouseButton(0))
            transform.position = new Vector3(GetMouseWorldPos().x, GetMouseWorldPos().y, 0);
    }

    // void OnMouseOver() {}

    private Vector2 GetMouseWorldPos()
    {
        Vector3 getMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(getMouse.x, getMouse.y);
    }
}
