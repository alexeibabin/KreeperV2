using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpashScreenLoadNext : MonoBehaviour
{

    public float fadeInTimer = 2f;
    public float onScreenTimer = 3f;
    public float fadeOutTimer = 2f;
    public float fadeMultiplier = 0.0001f;
    public Text text;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("FadeInLoadNext");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator FadeInLoadNext()
    {
        float timer = fadeInTimer;
        Color tempColor = text.color;
        tempColor.a = 0;
        Debug.Log("Faiding in");
        while (timer >= 0)
        {
            text.color = tempColor;
            yield return new WaitForSeconds(0.01f);
            tempColor.a = Mathf.Lerp(tempColor.a, 255, fadeMultiplier);
            timer -= 0.01f;
        }

        Debug.Log("Waiting " + onScreenTimer);
        yield return new WaitForSeconds(onScreenTimer);

        //timer = fadeOutTimer;
        //Debug.Log("Faiding out");
        //while (timer >= 0)
        //{
        //    tempColor.a = Mathf.Lerp(tempColor.a, 0, fadeMultiplier);
        //    text.color = tempColor;
        //    yield return new WaitForSeconds(0.01f);
        //    timer -= 0.01f;
        //}
        Application.LoadLevel(1);


    }
}
