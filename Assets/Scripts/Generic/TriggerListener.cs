using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerListener : MonoBehaviour
{
    [SerializeField] TriggerInformParent trigger;

    public event Action OnChildTriggerEnter;
    public event Action OnChildTriggerExit;

    private void Awake()
    {
        if(!trigger) trigger = GetComponentInChildren<TriggerInformParent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        trigger.OnObjectEnter += Enter;
        trigger.OnObjectExit += Exit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Enter()
    {
        OnChildTriggerEnter?.Invoke();
    }

    private void Exit()
    {
        OnChildTriggerExit?.Invoke();
    }

    private void OnDestroy()
    {
        trigger.OnObjectEnter -= Enter;
        trigger.OnObjectExit -= Exit;
    }
}
