using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        PauseSystem.OnPauseToggle += TogglePauseMenu;
    }


    private void TogglePauseMenu(bool isPaused)
    {
        if (DistanceTracker.Instance.GameFinished) return;
        pauseMenu.SetActive(isPaused);
    }

    private void OnDestroy()
    {
        PauseSystem.OnPauseToggle -= TogglePauseMenu;
    }
}
