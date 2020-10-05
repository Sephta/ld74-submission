using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUIObject : MonoBehaviour
{
    [Header("Object Data")]
    public GameObject objectToToggle = null;
    [SerializeField] public List<GameObject> toggleObjects = new List<GameObject>();
    public bool startState = false;
    public Vector3 originalPos = Vector3.zero;
    public Vector3 originalScale = Vector3.zero;
    public Vector3 newPos = Vector3.zero;
    public Vector3 newScale = Vector3.zero;

    [Header("Tween Data")]
    public LeanTweenType easeIn;
    public LeanTweenType easeOut;
    public float timeToTween = 0f;
    public float delayTime = 0f;


    void Awake()
    {
        if (objectToToggle == null)
            Debug.Log("Warning. reference to 'objectToToggle' on " + gameObject.name + " is null.");
    }

    // void OnEnable() {}
    void OnDisable()
    {
        // if (objectToToggle != null)
        //     LeanTween.scale(objectToToggle, Vector3.zero, 1f).setEase(LeanTweenType.linear);
    }

    void Start() 
    {
        if (objectToToggle != null)
            objectToToggle.SetActive(startState);
    }

    // void Update() {}
    // void FixedUpdate() {}

    public void ToggleObject()
    {
        if (objectToToggle != null)
        {
            if (!objectToToggle.activeSelf)
                objectToToggle.SetActive(!objectToToggle.activeSelf);
            else
            {
                if (objectToToggle.GetComponent<MoveUI>() != null)
                    objectToToggle.GetComponent<MoveUI>().DisableSelf();
            }
        }
    }

    public void ToggleObject(GameObject obj)
    {
        if (obj != null)
        {
            if (!obj.activeSelf)
                obj.SetActive(!obj.activeSelf);
            else
            {
                if (obj.GetComponent<MoveUI>() != null)
                    obj.GetComponent<MoveUI>().DisableSelf();
            }
        }
    }

    public void ToggleMultipleObjects()
    {
        if (toggleObjects.Count <= 0)
            return;
        
        foreach (GameObject ob in toggleObjects)
            ToggleObject(ob);
        // ob.SetActive(!ob.activeSelf);
    }

    public void ToggleMultipleObjectsOn()
    {
        if (toggleObjects.Count <= 0)
            return;
        
        foreach (GameObject ob in toggleObjects)
        {
            if (!ob.activeSelf)
                ob.SetActive(true);
        }
    }

    public void ToggleMultipleObjectsOff()
    {
        if (toggleObjects.Count <= 0)
            return;
        
        foreach (GameObject ob in toggleObjects)
        {
            if (ob.activeSelf)
                ob.SetActive(false);
        }
    }

    // Toggling an object that was moved
    public void ToggleOn_Moved()
    {
        if (objectToToggle != null)
        {
            if (!objectToToggle.activeSelf)
                objectToToggle.SetActive(!objectToToggle.activeSelf);
            else
                LeanTween.move(objectToToggle.GetComponent<RectTransform>(), originalPos, timeToTween).setDelay(delayTime).setEase(easeOut).setOnComplete(SetActiveFalse);
        }
    }

    // Toggling an object that was scaled
    public void ToggleOn_Scaled()
    {
        if (objectToToggle != null)
        {
            if (!objectToToggle.activeSelf)
                objectToToggle.SetActive(true);
            else
                ToggleOff_Scaled();
        }
    }

    public void ToggleOff_Scaled()
    {
        if (objectToToggle != null)
        {
            if (objectToToggle.activeSelf)
                LeanTween.scale(objectToToggle, originalScale, timeToTween).setDelay(delayTime).setEase(easeOut).setOnComplete(SetActiveFalse);
            else
                ToggleOn_Scaled();
        }
    }

    public void ScaleUp()
    {
        if (objectToToggle != null)
        {
            LeanTween.scale(objectToToggle, newScale, timeToTween).setDelay(delayTime).setEase(easeIn);
        }
    }

    public void ScaleDown()
    {
        if (objectToToggle != null)
        {
            LeanTween.scale(objectToToggle, originalScale, timeToTween).setDelay(delayTime).setEase(easeOut);
        }
    }

    void SetActiveFalse()
    {
        if (objectToToggle != null)
            objectToToggle.SetActive(false);
    }
}
