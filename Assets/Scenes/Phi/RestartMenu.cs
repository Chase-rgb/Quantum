using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    public static bool RestartMenuUp = false;

    public GameObject restartMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (RestartMenuUp)
            {
                Resume();
            }
            else
            {
                Restarted();
            }
                
        }
    }

    public void Resume()
    {
        restartMenuUI.SetActive(false);
        Time.timeScale = 1f;
        RestartMenuUp = false;

    }

    void Restarted()
    {
        restartMenuUI.SetActive(true);
        Time.timeScale = 0f;
        RestartMenuUp = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
