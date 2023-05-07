using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static event Action OnSwitchScene;

    public static SceneManage Instance { get; private set; }

    public void SwitchScene(string scene)
    {
        OnSwitchScene?.Invoke();
        SceneManager.LoadScene(scene);
    }
    public void Quit() => Application.Quit();
}
