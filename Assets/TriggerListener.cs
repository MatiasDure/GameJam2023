using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerListener : MonoBehaviour
{
    [SerializeField] TriggerInformParent trigger;

    public event Action OnChildTriggered;

    private void Awake()
    {
        if(!trigger) trigger = GetComponentInChildren<TriggerInformParent>();
        Debug.Log(trigger);
    }
    // Start is called before the first frame update
    void Start()
    {
        trigger.OnTriggered += Test;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Test()
    {
        OnChildTriggered?.Invoke();
    }

    private void OnDestroy()
    {
        trigger.OnTriggered -= Test;
    }
}
