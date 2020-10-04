using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointHandler : MonoBehaviour
{
    [ReadOnly] public GameObject _traveler = null;

    private float _timer = 10f;

    // void Awake() {}
    // void Start() {}
    void Update()
    {
        _timer -= Time.deltaTime;
        _timer = Mathf.Clamp(_timer, 0, 10f);

        if (_timer <= 0f)
            Destroy(this.gameObject);
    }
    // void FixedUpdate() {}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == _traveler)
        {
            Destroy(this.gameObject);
        }
    }
}
