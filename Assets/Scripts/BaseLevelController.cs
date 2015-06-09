using UnityEngine;
using System.Collections;

public abstract class BaseLevelController : MonoBehaviour {

    public int retries = 3;

    protected bool alarmStarted = false;

    protected abstract void ResetPlayerPosition();
    protected abstract IEnumerator RestartLevelCountdown();
    protected abstract IEnumerator DisplayGameOverSequence();
    protected abstract IEnumerator DisplayStartSequence();
    protected abstract IEnumerator DisplayWinSequence();

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
}
