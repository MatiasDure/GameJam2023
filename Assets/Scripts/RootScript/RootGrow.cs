using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(TriggerListener), typeof(BoxCollider), typeof(Movement))]
public class RootGrow : MonoBehaviour
{
    [SerializeField] BoxCollider colliderBox;
    [SerializeField] TriggerListener listener;
    [SerializeField] Vector3 rootGrowth;
    [SerializeField] ParticleSystem dirtParticle;

    public bool growing = false;

    [SerializeField] float growSpeed;
    [SerializeField] float growthAmount;
    [SerializeField] GameObject mesh;

    [SerializeField] RootGrow self;

    public bool isTwoStep;
    bool secondGrown;

    [SerializeField] float[] newBranchHeights;  

    [SerializeField] Movement movementScript; 

    Vector3 startScale;

    private void Awake()
    {
        if(!listener) listener = GetComponent<TriggerListener>();
        if(!colliderBox) colliderBox = GetComponent<BoxCollider>();
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
        if(isTwoStep) twoStep();
    }

    void twoStep()
    {
        Vector3 a = mesh.transform.localScale;
        Vector3 b = rootGrowth * growthAmount;
        b *= 0.8f;
        if (a.x > b.x && a.y > b.y && a.z > b.z && !secondGrown)
        {
            Vector3 r = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            float zRot = 3.69f;
            if (r.z == -90) zRot = 90;
            if (r.z == 90) zRot = -90;
            if (r.z == 0)
            {
                Vector3 p = transform.position;
                if(p.x > 0) zRot = 90;
                if (p.x < 0) zRot = -90;
                if (p.x == 0) return;
            }

            var newBranch = SproutPool.Instance.GetPooledRoot();
            newBranch.isTwoStep = false;

            int index = UnityEngine.Random.Range(0, newBranchHeights.Length);
            float num = newBranchHeights[index]; 

            newBranch.gameObject.SetActive(true);
            newBranch.transform.position = transform.position + transform.up * num;
            newBranch.transform.rotation = Quaternion.Euler(0, 0, zRot);
            newBranch.isTwoStep = false;
            secondGrown = true;
        }
    }

    void Grow()
    {
        mesh.transform.localScale = Vector3.Lerp(mesh.transform.localScale, rootGrowth * growthAmount, growSpeed);
        if (mesh.transform.localScale == rootGrowth * growthAmount) growing = false;
    } 

    private void Deactivate()
    {
        ResetRoot();
        this.gameObject.SetActive(false);
    }

    private void GrowRoot()
    {
        AudioManage.Instance.Play(AudioManage.sound.sprouts);
        if(dirtParticle != null ) dirtParticle.Play();
        StartCoroutine(CameraShake.Instance.Shake(.05f,.06f,.04f));
        growing = true; 
        colliderBox.size = new Vector3(rootGrowth.x, rootGrowth.y *2.5f, rootGrowth.z);
        UpdateCenter(new Vector3(rootGrowth.x, rootGrowth.y * 2.5f, rootGrowth.z));
    }

    private void ResetRoot()
    {
        growing = false;
        secondGrown = false;
        mesh.transform.localScale = startScale;
        colliderBox.size = Vector3.one;
        colliderBox.center = Vector3.zero;
        transform.localRotation = Quaternion.identity;
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
