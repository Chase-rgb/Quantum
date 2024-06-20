using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public void StartGame() {
        LevelLoader.instance.LoadLevelByName("Opening1");
    }

    public void ExitGame() {
        Application.Quit();
    }
}
