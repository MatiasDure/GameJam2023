using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    public static event Action<bool> OnPauseToggle;

    public static PauseSystem Instance { get; private set; }
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(Instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetPause();
        SceneManage.OnSwitchScene += ResetPause;
    }

    // Update is called once per frame
    void Update()
    {
        if (DistanceTracker.Instance.GameFinished) return;
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0 : 1;
        OnPauseToggle?.Invoke(IsPaused);
    }

    void ResetPause()
    {
        IsPaused = false;
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        SceneManage.OnSwitchScene -= ResetPause;
    }
}
