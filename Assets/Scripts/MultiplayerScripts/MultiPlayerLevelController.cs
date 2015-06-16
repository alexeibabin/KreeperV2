using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MultiPlayerLevelController : BaseLevelController
{

    public bool autoConnect = true;
    public byte version = 1;


    public GameObject standbyCamera;

    public GameObject localPlayerPrefab;
    public GameObject remotePlayerPrefab;

    public Transform ownerStartLocation;
    public Transform guestStartLocation;

	public FadeIn fadeInSequence;
	public FadeOut fadeOutSequence;


    public GameObject enemyPrefab;
    public Transform[] enemiesStartLocations;

    private RoomInfo[] roomsList;
    private bool alreadyInRoom = false;

    private bool connectOnUpdate = true;
    private bool localPlayerCreated = false;

	private ArrayList guards;

	private GameObject localPlayerGameObject;
	private GameObject remotePlayerGameObject;
	

    void Start()
    {
        PhotonNetwork.logLevel = PhotonLogLevel.Informational;
        PhotonNetwork.ConnectUsingSettings("0." + version);
        PhotonNetwork.autoJoinLobby = true;
		guards = new ArrayList ();
		StartCoroutine ("DisplayStartSequence");
    }

    
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }


    #region Networking stuff

	public void OnConnectedToMaster()
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
        if (alreadyInRoom)
        {
            return;
        }
        roomsList = PhotonNetwork.GetRoomList();
        bool foundRoom = false;
        RoomInfo freeRoom = null;
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        Debug.Log("Got room list: " + rooms.Length);
        foreach (RoomInfo room in rooms)
        {
            if (room.playerCount < 2)
            {
                foundRoom = true;
                freeRoom = room;
                break;
            }
        }

        alreadyInRoom = true;

        if (foundRoom)
        {
            Debug.Log("Found a free room, named " + freeRoom.name);
            PhotonNetwork.JoinRoom(freeRoom.name);
        }
        else
        {
            Debug.Log("Could not find a good room, creating a new one");
            PhotonNetwork.CreateRoom(null);
        }

    }


	public void OnCreatedRoom()
    {
        Debug.Log("Created a room " + PhotonNetwork.room.name);
    }

	public void OnJoinRandomRoom()
    {
        Debug.Log("Joined a random room");
    }

	public void OnJoinedRoom()
    {
        Debug.Log("Joined Room " + PhotonNetwork.room.name);

        if (standbyCamera)
        {
            standbyCamera.SetActive(false);
        }

        if (PhotonNetwork.isMasterClient)
        {
            EnableLocalPlayerComponents(ownerStartLocation);
            InitializeEnemies();
        }
        else
        {
            EnableLocalPlayerComponents(guestStartLocation);
        }
    }

	public void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.Log("Could not connect to PUN!");
        Debug.Log("Reason: " + cause);
    }

    #endregion

    #region Coroutines

    protected override IEnumerator RestartLevelCountdown()
    {
		fadeOutSequence.Activate ();
        yield return new WaitForSeconds(1);
		ResetPlayerPosition();
		fadeInSequence.Activate ();
		
    }
    protected override IEnumerator DisplayGameOverSequence()
    {
        yield return new WaitForSeconds(1);
    }
    protected override IEnumerator DisplayStartSequence()
    {
		fadeInSequence.Activate ();
        yield return new WaitForSeconds(1);
    }
    protected override IEnumerator DisplayWinSequence()
    {
        yield return new WaitForSeconds(1);
    }
    #endregion

    protected override void ResetPlayerPosition() {
		Vector3 startPostition;
		Quaternion startRotation;

		if (!PhotonNetwork.isMasterClient) {
			startPostition = ownerStartLocation.position;
			startRotation = ownerStartLocation.rotation;
		} else {
			startPostition = guestStartLocation.position;
			startRotation = guestStartLocation.rotation;
		}
		localPlayerGameObject.GetComponent<PlayerSightController>().SetPlayerIdle();
		localPlayerGameObject.transform.position = startPostition;
		localPlayerGameObject.GetComponent<PlayerSightController>().ResetWaypoints();
		
		if (--retries < 0)
		{
			Application.LoadLevel("MainMenu");
		}
		
	}

    private void EnableLocalPlayerComponents(Transform instantiateLocation)
    {
        localPlayerGameObject = PhotonNetwork.Instantiate(localPlayerPrefab.name, instantiateLocation.position, instantiateLocation.rotation, 0);
        localPlayerGameObject.GetComponent<CharacterController>().enabled = true;
        localPlayerGameObject.GetComponent<Cardboard>().enabled = true;
        localPlayerGameObject.SetActive(true);
		localPlayerGameObject.transform.FindChild("Human_Man").gameObject.SetActive(false);
		Debug.Log (localPlayerGameObject.transform.FindChild ("Head"));
        localPlayerGameObject.transform.FindChild("Head").gameObject.SetActive(true);
        localPlayerGameObject.transform.FindChild("Head").FindChild("Main Camera").gameObject.GetComponent<AudioListener>().enabled = true;
		
        //localPlayerGameObject.GetComponent<FirstPersonController>().enabled = true;
    }

	

    void InitializeEnemies()
    {
        GameObject tempEnemy;
        foreach (Transform enemyStartLocation in enemiesStartLocations)
        {
            tempEnemy = PhotonNetwork.Instantiate(enemyPrefab.name, enemyStartLocation.position, enemyStartLocation.rotation, 0);
            tempEnemy.GetComponent<Patrolling>().enabled = true;
            tempEnemy.GetComponent<Detection>().enabled = true;
            enemyStartLocation.gameObject.GetComponent<EnemyLocationSettingsContainer>().SetEnemyParameters(tempEnemy);
			guards.Add(tempEnemy);
        }
    }



}
