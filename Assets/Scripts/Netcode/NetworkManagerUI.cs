using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.Netcode;

public class NetworkManagerUI : NetworkBehaviour
{
    //[SerializeField]private Button serverBtn;
    [SerializeField]private Button hostBtn;
    [SerializeField]private Button clientBtn;
    public GameObject NetworkManagerPrefab;
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;

    private void Awake() 
    {
        Instantiate(NetworkManagerPrefab);
        hostBtn.onClick.AddListener(() => {
            Debug.Log("Host connecting");
            RemovePlayerControls();
            NetworkManager.Singleton.StartHost();
            PlayerManager.instance.currPlayer = 1;
            PlayerManager.instance.hostPlayer.Value = 1;
            NetworkManager.Singleton.OnClientConnectedCallback += SetupTempNetworking;
            //PlayerManager.instance.currPlayerObject = GameObject.Find("Player 1");
        });

        clientBtn.onClick.AddListener(() => {
            Debug.Log("Client connecting");
            RemovePlayerControls();
            NetworkManager.Singleton.StartClient();
            PlayerManager.instance.currPlayer = 2;
            NetworkManager.Singleton.OnClientConnectedCallback += SetupTempNetworking;
            //PlayerManager.instance.currPlayerObject = GameObject.Find("Player 2");
        });
    }


    private void SetupTempNetworking(ulong clientId)
    {
        foreach (GameObject cloneObject in GameObject.FindGameObjectsWithTag("clone")) { Destroy(cloneObject); }
        Screen.SetResolution(640, 480, false);
        GameManager.instance.SetNetworkedScreen(true);
        GameManager.instance.SetUpLevel();
        GameObject currPlayer = PlayerManager.instance.currPlayerObject;
        Destroy(currPlayer.GetComponent<MovementWASD>());
        Destroy(currPlayer.GetComponent<MovementArrows>());
        Destroy(currPlayer.GetComponent<PlayerMovement>());
        PlayerMovement pm = currPlayer.AddComponent<PlayerMovement>();
        pm.dashParticle = FindParticleSystem("DashParticles", currPlayer);
        pm.momentumOutParticle = FindParticleSystem("MomentumTransferParticles", currPlayer);
        pm.momentumInParticle = FindParticleSystem("MomentumRecieveParticle", currPlayer);
        if (currPlayer.name == "Player 1") 
        { 
            pm.jumpParticle = FindParticleSystem("JumpPartRed", currPlayer);
            pm.wallJumpParticle = FindParticleSystem("SlideParticleParent/WallJumpRed", currPlayer);
            pm.slideParticle = FindParticleSystem("SlideParticleParent/SlideRed", currPlayer);
            pm.world = 1;
        } else
        {
            pm.jumpParticle = FindParticleSystem("JumpPartPurp", currPlayer);
            pm.wallJumpParticle = FindParticleSystem("SlideParticleParent/WallJumpPurp", currPlayer);
            pm.slideParticle = FindParticleSystem("SlideParticleParent/SlidePurp", currPlayer);
            pm.world = 2;
        }
    }

    private ParticleSystem FindParticleSystem(string systemName, GameObject playerObject)
    {
        Debug.Log($"Particle search in {playerObject.name}");
        Transform systemTransform = playerObject.transform.Find(systemName);
        if (systemTransform != null)
        {
            return systemTransform.GetComponent<ParticleSystem>();
        }
        else
        {
            Debug.LogError("ParticleSystem with name " + systemName + " not found on Player.");
            return null;
        }
    }

    private void RemovePlayerControls()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Destroy(player.GetComponent<MovementWASD>());
            Destroy(player.GetComponent<MovementArrows>());
            Destroy(player.GetComponent<PlayerMovement>());
        }
    }

    //private void SetupTempNetworking(ulong clientId)
    //{
    //    Debug.Log($"Connected! With client id: {clientId}");
    //    Debug.Log($"Current id: {NetworkManager.Singleton.LocalClientId}");
    //    Debug.Log($"Host val: {PlayerManager.instance.hostPlayer.Value}");
    //    if (clientId != NetworkManager.Singleton.LocalClientId) return;
    //    NetworkManager.Singleton.OnClientConnectedCallback -= SetupTempNetworking;
    //    SetupTempNetworkingHelper(0);
    //}

    //private void SetupTempNetworkingHelper(ulong clientId)
    //{
    //    // Remove previous clones that get spawned from initial level setup
    //    foreach (GameObject cloneObject in GameObject.FindGameObjectsWithTag("clone")) { Destroy(cloneObject); }
    //    Screen.SetResolution(640, 480, false);
    //    GameManager.instance.SetNetworkedScreen(true);
    //    ReplacePlayers();
    //    GameManager.instance.SetUpLevel();
    //    //GameObject currPlayer = PlayerManager.instance.currPlayerObject;
    //}

    // Game instantiates like the players are splitscreen.
    // To get Networking working, the players have to essentially be "reset" i.e. replaced
    // Ngl, the only reason why this is here is because of how particles work
    //private void ReplacePlayers()
    //{
    //    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    //    foreach (GameObject player in players)
    //    {
    //        GameObject newPlayer;
    //        Debug.Log($"Is Host? {IsHost}");
    //        if (IsHost)
    //        {
    //            if (player.name == "Player 1")
    //            {
    //                newPlayer = Instantiate(Player1Prefab, player.transform.position, player.transform.rotation, player.transform.parent);
    //            }
    //            else
    //            {
    //                newPlayer = Instantiate(Player2Prefab, player.transform.position, player.transform.rotation, player.transform.parent);
    //            }
    //            Destroy(player);
    //            Debug.Log($"Removing {player.name}");
    //            newPlayer.GetComponent<NetworkObject>().Spawn();
    //        }
    //        Destroy(player);
    //    }
    //}




}
