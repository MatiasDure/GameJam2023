using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SproutPool : MonoBehaviour
{
    [SerializeField] RootGrow rootPrefab;
    [SerializeField] uint amountToSpawn;
    [SerializeField] Vector3[] growthRoots;
    [SerializeField] uint lanesUsed;

    private List<RootGrow> pooledRoots;

    public static SproutPool Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        pooledRoots = new();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < amountToSpawn; i++)
        {
            RootGrow temp = Instantiate(rootPrefab);
            pooledRoots.Add(temp);
            temp.gameObject.SetActive(false);
        }
    }

    public RootGrow GetPooledRoot()
    {
        foreach(var root in pooledRoots)
        {
            if (!root.gameObject.activeInHierarchy)
            {
                root.SetRootGrowth(AssignRandomGrowth());
                var bla = AssignRandomPosition();
                Debug.Log("random pos assigned: " + bla);
                root.transform.position = bla;
                Debug.Log("x value: "+ root.transform.position.x);
                return root;
            }
        }
        return InstantiateNewRoot();
    }

    private RootGrow InstantiateNewRoot()
    {
        RootGrow temp = Instantiate(rootPrefab);
        temp.gameObject.SetActive(false);
        temp.SetRootGrowth(AssignRandomGrowth());
        temp.transform.position = AssignRandomPosition();
        pooledRoots.Add(temp);
        return temp;
    }

    private Vector3 AssignRandomPosition() => new(Mathf.Round(Random.Range(-lanesUsed, lanesUsed+1)), 0,0);

    private Vector3 AssignRandomGrowth() => growthRoots[Random.Range(0, growthRoots.Length)];

}
