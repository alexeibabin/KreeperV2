using UnityEngine.UI;
using UnityEngine;


public class LevelController : Photon.MonoBehaviour
{

    public bool autoConnect = true;
    public byte version = 1;
    public Text statusText;
    public Text bigBody;
    public GameObject standbyCamera;

    public GameObject localPlayerPrefab;
    public GameObject remotePlayerPrefab;

    public Transform ownerStartLocation;
    public Transform guestStartLocation;

    public GameObject localPlayerGameObject;
    public GameObject remotePlayerGameObject;

    private bool connectOnUpdate = true;
    private bool localPlayerCreated = false;


    // Use this for initialization
    public override void Start()
    {
        PhotonNetwork.offlineMode = true;
        Debug.Log(PhotonNetwork.offlineMode);
        Debug.Log(PhotonNetwork.autoJoinLobby);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (connectOnUpdate && autoConnect && !PhotonNetwork.connected)
        {
            Debug.Log("Trying to connect to PUN with the default settings");
            connectOnUpdate = false;
            //PhotonNetwork.autoJoinLobby = false;
            PhotonNetwork.ConnectUsingSettings("0." + version);
        }
    }

    #region Networking stuff

    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster was called by PUN. Now we can join a room, calling PhotonNetwork.JoinRandomRoom()");
        //PhotonNetwork.JoinLobby();
        if (PhotonNetwork.GetRoomList().Length > 0)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.CreateRoom("Random Room!!!");
        }

    }

    public virtual void OnCreatedRoom()
    {
        Debug.Log("Created a room and joined");
        CreateLocalPlayer();
    }

    public virtual void OnJoinRandomRoom()
    {
        Debug.Log("Joined a random room");
        CreateOtherPlayer();
        CreateLocalPlayer();
    }

    public virtual void OnJoinRoom()
    {
        Debug.Log("Some player has joined a room");
        CreateOtherPlayer();
        CreateLocalPlayer();
    }

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.Log("Could not connect to PUN!");
        Debug.Log("Reason: " + cause);
    }

    #endregion

    private void CreateLocalPlayer()
    {
        if (!localPlayerCreated)
        {
            if (PhotonNetwork.isMasterClient)
            {
                CreatePlayer(localPlayerPrefab, ownerStartLocation);
            }
            else
            {
                CreatePlayer(localPlayerPrefab, guestStartLocation);
            }
            localPlayerCreated = true;

        }
    }

    private void CreateOtherPlayer()
    {
        if (PhotonNetwork.isMasterClient)
        {
            // Instantiate the remote guest player
            CreatePlayer(remotePlayerPrefab, guestStartLocation);
        }
        else
        {
            // You are the guest in the game, instantiate your player
            CreatePlayer(remotePlayerPrefab, ownerStartLocation);
        }
    }

    private void CreatePlayer(GameObject prefab, Transform startTransform)
    {
        int group = 0;
        PhotonNetwork.Instantiate(prefab.name, startTransform.position, startTransform.rotation, group);
    }

}
