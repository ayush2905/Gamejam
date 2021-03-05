using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleLevelManager : MonoBehaviour
{
    public void LoadMenu()
    {
        LevelManager.instance.GoHome();
    }

    public void LoadLevel(string levelName)
    {
        LevelManager.instance.LoadLevel(levelName);
    }
}
