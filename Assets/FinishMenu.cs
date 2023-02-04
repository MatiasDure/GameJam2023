using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishMenu : MonoBehaviour
{
    [SerializeField] GameObject finishScreen;
    [SerializeField] TextMeshProUGUI distanceText;

    const string DISTANCE_ACHIEVED = "Distance Traveled: ";

    private void Start()
    {
        DistanceTracker.OnFinish += DisplayScore;
    }

    private void DisplayScore(string distance)
    {
        PauseSystem.Instance.TogglePause();
        finishScreen.SetActive(true);
        distanceText.text = DISTANCE_ACHIEVED + distance;
    }

    private void OnDestroy()
    {
        DistanceTracker.OnFinish -= DisplayScore;
    }

}
