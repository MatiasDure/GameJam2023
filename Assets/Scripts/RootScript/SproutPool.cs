using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SproutPool : MonoBehaviour
{
    [SerializeField] RootGrow rootPrefab;
    [SerializeField] uint amountToSpawn;
    [SerializeField] Vector3[] growthRoots;
    [SerializeField] int lanesUsed;
    [SerializeField] float[] rotations;

    [SerializeField] int distanceToSpawn = 16;

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
                var ranRotation = AssignRandomRotation();

                //assigning position based on rotation
                Vector3Int ranPos;
                if (ranRotation == 0) ranPos = AssignRandomPosition();
                else ranPos = ranRotation > 0 ? new(lanesUsed, 0, distanceToSpawn) : new(-lanesUsed, 0, distanceToSpawn);

                root.transform.position = ranPos;
                root.transform.rotation = Quaternion.Euler(0, root.transform.rotation.y, ranRotation);
                root.isTwoStep = true;
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

    private float AssignRandomRotation() => rotations[Random.Range(0, rotations.Length)];

    private Vector3Int AssignRandomPosition() => new(Random.Range(-lanesUsed, lanesUsed+1), 0, distanceToSpawn);

    private Vector3 AssignRandomGrowth() => growthRoots[Random.Range(0, growthRoots.Length)];

}
