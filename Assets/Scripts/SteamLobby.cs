using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using Steamworks;

using Mirror;
using UnityEngine.UI;

public class SteamLobby : MonoBehaviour

{

    protected Callback<LobbyCreated_t> lobbyCreated;

    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;

    protected Callback<LobbyEnter_t> lobbyEntered;

    private NetworkManager networkManager;


    private const string HostAddressKey = "HostAddress";


    [SerializeField]
    private GameObject buttons;

public void HostLobby()

    {

        buttons.SetActive(false);

        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, networkManager.maxConnections);

    }

    // Start is called before the first frame update

    void Start()

    {
        

        networkManager = GetComponent<NetworkManager>();

        if (!SteamManager.Initialized)
        { Debug.Log("Steam Manager not initialized"); return; }

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);

        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);

        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);

    }

    private void OnLobbyCreated(LobbyCreated_t callback)

    {

        if (callback.m_eResult != EResult.k_EResultOK)

        {

            buttons.SetActive(true);

            Debug.LogError("Failed to Create Lobby");

            return;

        }

        networkManager.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
     

    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)

    {

        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);

    }

    private void OnLobbyEntered(LobbyEnter_t callback)

    {

        if (NetworkServer.active) { return; }

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);

        networkManager.networkAddress = hostAddress;

        networkManager.StartClient();

        Debug.LogError(hostAddress);

       


    }
   
    void Update()
    {
        if (!NetworkClient.ready && !NetworkClient.isLoadingScene && NetworkClient.isConnected)
        {
            NetworkClient.Ready();//Marks Client as ready making Client available to recieve [Command] messages.

            Debug.LogError(NetworkClient.ready);
        }

    }

}