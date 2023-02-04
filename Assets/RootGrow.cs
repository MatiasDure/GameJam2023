using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerListener), typeof(BoxCollider))]
public class RootGrow : MonoBehaviour
{
    [SerializeField] BoxCollider colliderBox;
    [SerializeField] TriggerListener listener;
    [SerializeField] Vector3 rootGrowth;

    bool growing = false;

    [SerializeField] float growSpeed;
    [SerializeField] float growthAmount;
    [SerializeField] GameObject mesh;

    Vector3 startScale;

    

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
        startScale = mesh.transform.localScale;
        listener.OnChildTriggerEnter += GrowRoot;
        listener.OnChildTriggerExit += Deactivate;
    }

    private void FixedUpdate()
    {
        if (growing) Grow(); 
        
    }

    void Grow()
    {
        mesh.transform.localScale = Vector3.Lerp(mesh.transform.localScale, rootGrowth * growthAmount, growSpeed);
        if (mesh.transform.localScale == rootGrowth * growthAmount) growing = false;

      //  Debug.Log("bruhsdfjhsdfjh " + mesh.transform.localScale); 
    } 

    private void Deactivate()
    {
        ResetRoot();
        this.gameObject.SetActive(false);
    }

    private void GrowRoot()
    {
        StartCoroutine(CameraShake.Instance.Shake(.05f,.06f,.04f));
       growing = true; 
        colliderBox.size = rootGrowth;
        UpdateCenter(rootGrowth);
    }

    private void ResetRoot()
    {
        growing = false;
        mesh.transform.localScale = startScale;
        colliderBox.size = Vector3.one;
        colliderBox.center = Vector3.zero;
    }

    private void UpdateCenter(Vector3 newSize) => colliderBox.center = new Vector3(FormulaForCenter(newSize.x, true), FormulaForCenter(newSize.y), FormulaForCenter(newSize.z));

    private float FormulaForCenter(float newSize, bool canNegate = false)
    {
        float result = (newSize - 1) / 2;
        if(canNegate) return this.transform.position.x > 0 ? result : -result;
        return result;
    }

    public void SetRootGrowth(Vector3 growth) => rootGrowth = growth;

    private void OnDestroy()
    {
        listener.OnChildTriggerEnter -= GrowRoot;
        listener.OnChildTriggerExit -= Deactivate;
    }
}
