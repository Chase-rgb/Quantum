using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyContinue : MonoBehaviour
{
    public bool start = false;
    public string next;

    private bool removed = false;

    private void Update()
    {
        // IK ITS BAD
        if (Input.anyKey)
        {
            if (!start)
            {
                MoveOn();
            }
            else
            {
                MoveOnStart();
            }
        }
    }

    public void MoveOn()
    {
        GameManager.instance.cutscene = true;
        if (next == "StartMenu" && !removed) {
            GameManager.instance.RemoveDoNotDestroyObjects();
            MusicManager.instance.SetMainMenuMusic();
            removed = true;
        }
        LevelLoader.instance.LoadLevelByName(next);
    }

    public void MoveOnStart()
    {
        LevelLoader.instance.LoadLevelByName(next);
    }
}
