using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    public static event Action<string> OnFinish;

    float timer = 0;
    string finalTime;
    
    public static DistanceTracker Instance { get; private set; }
    public bool GameFinished { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }

    private void Start()
    {
        GameFinished = false;
        PlayerCollision.OnPlayerHit += SetTimer;
    }

    private void Update()
    {
        if (GameFinished) return;
        timer += Time.deltaTime;
    }

    void SetTimer()
    {
        //use this to ignore pause
        GameFinished = true;
        finalTime = timer.ToString("0.00");
        OnFinish?.Invoke(finalTime);
    }

    private void OnDestroy()
    {
        PlayerCollision.OnPlayerHit -= SetTimer;
    }
}
