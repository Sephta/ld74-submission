using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUI : MonoBehaviour
{
    [Header("Object and Tween Data")]
    public RectTransform objectToTween = null;
    public LeanTweenType easeIn;
    public float timeToTween = 0f;
    public float delayTime = 0f;

    [Header("Scale Data")]
    public Vector3 desiredScale = Vector3.zero;

    void Awake()
    {
        if (objectToTween == null)
            Debug.Log("Warning. reference to 'objectToTween' on " + gameObject.name + " is null.");
    }

    void OnEnable()
    {
        if (objectToTween != null)
        {
            LeanTween.scale(objectToTween, desiredScale, timeToTween).setDelay(delayTime).setEase(easeIn);
        }
    }

    // void OnDisable() {}

    // void Start() {}
    // void Update() {}
    // void FixedUpdate() {}
}
