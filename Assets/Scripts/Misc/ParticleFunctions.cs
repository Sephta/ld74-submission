using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFunctions : MonoBehaviour
{
    public ParticleSystem _system = null;

    void Awake()
    {
        if (transform.GetChild(0).GetComponent<ParticleSystem>() != null)
            _system = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    // void Start() {}

    void Update()
    {
        // If there are no more particles alive in the system, destroy self (system)
        if (_system != null && !_system.IsAlive())
            Destroy(gameObject);
    }

    // void FixedUpdate() {}
}
