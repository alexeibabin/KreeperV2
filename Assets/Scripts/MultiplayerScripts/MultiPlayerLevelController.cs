using UnityEngine.UI;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MultiPlayerLevelController : MonoBehaviour
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

	private RoomInfo[] roomsList;
	private bool alreadyInRoom=false;

    private bool connectOnUpdate = true;
    private bool localPlayerCreated = false;

	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

    // Use this for initialization
    void Start()
    {
		PhotonNetwork.logLevel = PhotonLogLevel.Informational;
		PhotonNetwork.ConnectUsingSettings("0." + version);
		PhotonNetwork.autoJoinLobby = true;

    }

//
//    {
//        if (connectOnUpdate && autoConnect && !PhotonNetwork.connected)
//        {
//            Debug.Log("Trying to connect to PUN with the default settings");
//            connectOnUpdate = false;
//            //PhotonNetwork.autoJoinLobby = false;
//        }
//    }
//
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

	void OnReceivedRoomListUpdate()
	{
		if (alreadyInRoom) {
			return;
		}
		roomsList = PhotonNetwork.GetRoomList();
		bool foundRoom = false;
		RoomInfo freeRoom = null; 	
		RoomInfo[] rooms = PhotonNetwork.GetRoomList();
		Debug.Log("Got room list: " + rooms.Length);
		foreach(RoomInfo room in rooms){
			if (room.playerCount < 2){
				foundRoom = true;
				freeRoom = room;
				break;
			}
		}
		
		alreadyInRoom = true;
		
		if (foundRoom){
			Debug.Log("Found a free room, named "+ freeRoom.name);
			PhotonNetwork.JoinRoom(freeRoom.name);
		}else{
			Debug.Log("Could not find a good room, creating a new one");
			PhotonNetwork.CreateRoom(null);
		}
		
	}


	public virtual void OnCreatedRoom()
    {
		Debug.Log("Created a room "+ PhotonNetwork.room.name);
    }

    public virtual void OnJoinRandomRoom()
    {
        Debug.Log("Joined a random room");
    }

	public virtual void OnJoinedRoom()
    {
		Debug.Log("Joined Room "+ PhotonNetwork.room.name);

		if (standbyCamera)
		{
			standbyCamera.SetActive(false);
		}

		if (PhotonNetwork.isMasterClient) {
			EnableLocalPlayerComponents (ownerStartLocation);
		} else {
			EnableLocalPlayerComponents (guestStartLocation);
		}
    }

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.Log("Could not connect to PUN!");
        Debug.Log("Reason: " + cause);
    }

    #endregion


	private void EnableLocalPlayerComponents(Transform instantiateLocation){
		localPlayerGameObject = PhotonNetwork.Instantiate (localPlayerPrefab.name, instantiateLocation.position, instantiateLocation.rotation, 0);
		localPlayerGameObject.GetComponent<MeshRenderer> ().enabled = false;
		localPlayerGameObject.GetComponent<CharacterController> ().enabled = true;
		localPlayerGameObject.GetComponent<Cardboard> ().enabled = true;
		localPlayerGameObject.SetActive (true);
		localPlayerGameObject.transform.FindChild ("Head").gameObject.SetActive (true);
		localPlayerGameObject.transform.FindChild ("Head").FindChild ("Main Camera").gameObject.GetComponent<AudioListener> ().enabled = true;
		localPlayerGameObject.GetComponent<FirstPersonController> ().enabled = true;
	}


    /*private void CreateOtherPlayer()
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

    private void CreatePlayer(GameObject prefab, Transform startTransform)
    {
        int group = 0;
        if (standbyCamera)
        {
            standbyCamera.SetActive(false);
        }
        PhotonNetwork.Instantiate(prefab.name, startTransform.position, startTransform.rotation, group);
    }*/

}
