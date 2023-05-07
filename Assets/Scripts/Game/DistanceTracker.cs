using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] float speed;
    [SerializeField] float[] checkPoints;

    public static event Action<string> OnFinish;
    public static event Action OnMarkPassed;

    float distance = 0;
    string finalTime;
    uint currentIndex = 0;

    const string DISTANCE = "Distance: ";
    
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
        distance += Time.deltaTime * speed;
        UpdateText(distance.ToString("0.0"));
        CheckMark();
    }

    void UpdateText(string newTxt) => distanceText.text = DISTANCE + newTxt;

    void SetTimer()
    {
        //use this to ignore pause
        distanceText.gameObject.SetActive(false);
        GameFinished = true;
        finalTime = distance.ToString("0.0");
        OnFinish?.Invoke(finalTime);
    }

    void CheckMark()
    {
        if (currentIndex >= checkPoints.Length) return;
        if(distance >= checkPoints[currentIndex])
        {
            OnMarkPassed?.Invoke();
            currentIndex++;
        }
    }

    private void OnDestroy()
    {
        PlayerCollision.OnPlayerHit -= SetTimer;
    }
}
