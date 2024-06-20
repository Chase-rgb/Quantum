using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyContinue : MonoBehaviour
{
    public bool start = false;
    public string next;

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
        if (next == "StartMenu") {
            GameManager.instance.RemoveDoNotDestroyObjects();
        }
        LevelLoader.instance.LoadLevelByName(next);
    }

    public void MoveOnStart()
    {
        LevelLoader.instance.LoadLevelByName(next);
    }
}
