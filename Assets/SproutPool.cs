using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SproutPool : MonoBehaviour
{
    [SerializeField] RootGrow rootPrefab;
    [SerializeField] uint amountToSpawn;

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
            if (!root.gameObject.activeInHierarchy) return root;
        }
        return InstantiateNewRoot();
    }

    private RootGrow InstantiateNewRoot()
    {
        RootGrow temp = Instantiate(rootPrefab);
        temp.gameObject.SetActive(false);
        pooledRoots.Add(temp);
        return temp;
    }
}
