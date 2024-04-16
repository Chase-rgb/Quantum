using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Netcode;
using UnityEngine;

public class PauseMenuManager : NetworkBehaviour
{
    public static PauseMenuManager instance { get; private set; }

    public delegate void OnVariableChangeDelegate(bool newVal);
    public event OnVariableChangeDelegate OnVariablePauseChange;
    private bool m_paused = false;
    public bool paused
    {
        get { return m_paused; }
        set
        {
            if (m_paused == value) return;
            m_paused = value;
            if (OnVariablePauseChange != null) OnVariablePauseChange(m_paused);
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    [ClientRpc]
    public void TogglePauseClientRpc() { TogglePause(); }

    [ServerRpc(RequireOwnership = false)]
    public void TogglePauseServerRpc() { TogglePauseClientRpc(); }

    // Does the pause variable automatically update between the two? like is that wat is happening?
    public void TogglePause()
    {
        instance.paused = !instance.paused;

        PauseMenu.instance.TogglePause(instance.paused);
    }

    [ClientRpc]
    public void RestartClientRpc() { Restart(); }

    [ServerRpc(RequireOwnership = false)]
    public void RestartServerRpc() { RestartClientRpc(); }

    // Does the pause variable automatically update between the two? like is that wat is happening?
    public void Restart()
    {
        PauseMenu.instance.Restart();
    }

    [ClientRpc]
    public void ToMainMenuClientRPC() { ToMainMenu(); }

    [ServerRpc(RequireOwnership = false)]
    public void ToMainMenuServerRPC() { ToMainMenuClientRPC(); }

    // Does the pause variable automatically update between the two? like is that wat is happening?
    public void ToMainMenu()
    {
        PauseMenu.instance.ToMainMenu();
    }

    [ClientRpc]
    public void QuitClientRPC() { Quit(); }

    [ServerRpc(RequireOwnership = false)]
    public void QuitServerRPC() { QuitClientRPC(); }

    // Does the pause variable automatically update between the two? like is that wat is happening?
    public void Quit()
    {
        if (PauseMenu.instance.quit)
        {
            PauseMenu.instance.Quit();
        } else
        {
            PauseMenu.instance.ToMainMenu();
        }
    }
}
