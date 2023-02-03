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
        listener.OnChildTriggerEnter += GrowRoot;
        listener.OnChildTriggerExit += Deactivate;
    }

    private void Deactivate()
    {
        ResetRoot();
        this.gameObject.SetActive(false);
    }

    private void GrowRoot()
    {
        colliderBox.size = rootGrowth;
        UpdateCenter(rootGrowth);
    }

    private void ResetRoot()
    {
        colliderBox.size = Vector3.one;
        colliderBox.center = Vector3.zero;
    }

    private void UpdateCenter(Vector3 newSize) => colliderBox.center = new Vector3(FormulaForCenter(newSize.x), FormulaForCenter(newSize.y), FormulaForCenter(newSize.z));

    private float FormulaForCenter(float newSize) => (newSize - 1) / 2; 

    public void SetRootGrowth(Vector3 growth) => rootGrowth = growth;

    private void OnDestroy()
    {
        listener.OnChildTriggerEnter -= GrowRoot;
        listener.OnChildTriggerExit -= Deactivate;
    }
}
