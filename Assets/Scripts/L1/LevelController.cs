using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : BaseLevelController
{

	public FadeIn fadeInSequence;
	public FadeOut fadeOutSequence;

    public GameObject playerPrefab;
    public GameObject playerObject;
    public Transform playerSpawnPoint;

    public GameObject visualAlarm;
    public AudioSource audioAlarm;

    public float levelRestartCountdown = 3f;

    private bool looseStatus = false;
    private bool winStatus = false;
    private bool statrtStatus = true;

	private bool coroutineStarted = false;


    private bool startSequenceStarted = true;
    private bool gameoverSequenceStarted = false;
    
    void Start(){
        if (playerPrefab){
            playerObject = (GameObject) Instantiate(playerPrefab,playerSpawnPoint.position,playerSpawnPoint.rotation);
        }else{
        	playerObject = GameObject.FindWithTag("Player");
        }

    }

    public void SetPlayerFullyDetected()
    {
        looseStatus = true;
    }

    #region Coroutines
    protected override IEnumerator RestartLevelCountdown()
    {
        yield return new WaitForSeconds(levelRestartCountdown);
        SetPlayerFullyDetected();
		fadeOutSequence.Activate ();

        alarmStarted = false;
		coroutineStarted = false;
    }
    protected override IEnumerator DisplayGameOverSequence()
    {
        Debug.Log("Gameover sequence started");
        yield return new WaitForSeconds(3f);
        ResetPlayerPosition();
		fadeInSequence.Activate ();
		coroutineStarted = false;
    }
    protected override IEnumerator DisplayStartSequence()
    {
        Debug.Log("Start sequence initiated");
		fadeInSequence.Activate ();
        yield return new WaitForSeconds(1f);
		coroutineStarted = false;
    }
    protected override IEnumerator DisplayWinSequence()
    {
        Debug.Log("Win sequence started");
        yield return new WaitForSeconds(3f);
		coroutineStarted = false;
    }
    #endregion

    protected void HideGameOverSequence() { }
    protected void HideCountdownTimer() { }

    void Update()
    {
        if (looseStatus)
        {
			if (!coroutineStarted) 
            	StartCoroutine("DisplayGameOverSequence");
            looseStatus = false;
        }

        if (statrtStatus)
        {
			if (!coroutineStarted)
            	StartCoroutine("DisplayStartSequence");
            statrtStatus = false;
        }
    }

    protected override void ResetPlayerPosition()
    {
        playerObject.GetComponent<PlayerSightController>().SetPlayerIdle();
        playerObject.transform.position = playerSpawnPoint.transform.position;
		playerObject.GetComponent<PlayerSightController>().ResetWaypoints();

        if (--retries < 0)
        {
            Application.LoadLevel("MainMenu");
        }

    }
}
