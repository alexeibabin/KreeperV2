using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour
{

    public GameObject player;
    public GameObject playerSpawnPoint;

    public GameObject visualAlarm;
    public AudioSource audioAlarm;

    public float levelRestartCountdown = 3f;

    private bool looseStatus = false;
    private bool winStatus = false;
    private bool statrtStatus = false;


    private bool startSequenceStarted = true;
    private bool gameoverSequenceStarted = false;
    private bool alarmStarted = false;

    public void SetPlayerFullyDetected()
    {
        looseStatus = true;
    }

    public void StartCountdown()
    {
        if (!alarmStarted)
        {
            StartAlarmAudio();
            StartAlarmVisual();
            StartCoroutine("RestartLevelCountdown");
            alarmStarted = true;
        }
    }
    public void StopCountdown()
    {
        if (alarmStarted)
        {
            StopAlarmAudio();
            StopAlarmVisual();
            StopCoroutine("RestartLevelCountdown");
            alarmStarted = false;
        }
    }

    protected void StartAlarmVisual()
    {
        //visualAlarm.SetActive(true);
    }
    protected void StopAlarmVisual()
    {
        //visualAlarm.SetActive(false);
    }
    protected void StartAlarmAudio()
    {
        //TODO: change this to 'fast' fadein
        //audioAlarm.Play();
    }
    protected void StopAlarmAudio()
    {
        //TODO: change this to 'fast' fadeout
        //audioAlarm.Stop();
    }

    #region Coroutines
    IEnumerator RestartLevelCountdown()
    {
        yield return new WaitForSeconds(levelRestartCountdown);
        SetPlayerFullyDetected();
        alarmStarted = false;
    }
    IEnumerator DisplayGameOverSequence()
    {
        Debug.Log("Gameover sequence started");
        yield return new WaitForSeconds(3f);
        ResetPlayerPosition();
    }
    IEnumerator DisplayStartSequence()
    {
        Debug.Log("Start sequence initiated");
        yield return new WaitForSeconds(3f);
    }
    IEnumerator DisplayWinSequence()
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

    private void ResetPlayerPosition()
    {
        player.transform.position = playerSpawnPoint.transform.position;
        //foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        //{
        //    player.transform.position = pal
        //}
    }
}
