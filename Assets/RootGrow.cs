using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerListener),typeof(BoxCollider))]
public class RootGrow : MonoBehaviour
{
    [SerializeField] BoxCollider colliderBox;
    [SerializeField] TriggerListener listener;
    [SerializeField] Vector3 rootGrowth;

    private void Awake()
    {
        if(!listener) listener = GetComponent<TriggerListener>();
        if(!colliderBox) colliderBox = GetComponent<BoxCollider>();
        Debug.Log(listener);
        Debug.Log(colliderBox);
    }
    // Start is called before the first frame update
    void Start()
    {
        listener.OnChildTriggered += GrowRoot;
    }

    private void GrowRoot()
    {
        colliderBox.size = rootGrowth;
        UpdateCenter(rootGrowth);
    }

    private void UpdateCenter(Vector3 newSize) => colliderBox.center = new Vector3(FormulaForCenter(newSize.x), FormulaForCenter(newSize.y), FormulaForCenter(newSize.z));

    private float FormulaForCenter(float newSize) => (newSize - 1) / 2; 

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        listener.OnChildTriggered -= GrowRoot;
    }
}
