using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : BaseLevelController
{

    public GameObject playerPrefab;
    public GameObject playerObject;
    public Transform playerSpawnPoint;

    public GameObject visualAlarm;
    public AudioSource audioAlarm;

    public float levelRestartCountdown = 3f;

    private bool looseStatus = false;
    private bool winStatus = false;
    private bool statrtStatus = false;


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
        alarmStarted = false;
    }
    protected override IEnumerator DisplayGameOverSequence()
    {
        Debug.Log("Gameover sequence started");
        yield return new WaitForSeconds(3f);
        ResetPlayerPosition();
    }
    protected override IEnumerator DisplayStartSequence()
    {
        Debug.Log("Start sequence initiated");
        yield return new WaitForSeconds(3f);
    }
    protected override IEnumerator DisplayWinSequence()
    {
        Debug.Log("Win sequence started");
        yield return new WaitForSeconds(3f);
    }
    #endregion

    protected void HideGameOverSequence() { }
    protected void HideCountdownTimer() { }

    void Update()
    {
        if (looseStatus)
        {
            StartCoroutine("DisplayGameOverSequence");
            looseStatus = false;
        }

        if (statrtStatus)
        {
            StartCoroutine("DisplayStartSequence");
            statrtStatus = false;
        }
    }

    protected override void ResetPlayerPosition()
    {
        playerObject.GetComponent<PlayerSightController>().SetPlayerIdle();
        playerObject.transform.position = playerSpawnPoint.transform.position;
        
        if (--retries < 0)
        {
            Application.LoadLevel("MainMenu");
        }

    }
}
