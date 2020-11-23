using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextWritter))]
public class GlobalUIElemnts : Singleton<GlobalUIElemnts>
{
    public GameObject messageObject;
    public TextMeshProUGUI messageText;

    private TextWritter textWritter;

    private bool textFinished = false;

    private PauseSettings pauseSettings;

    private void Start()
    {
        textWritter = gameObject.GetComponent<TextWritter>();
        messageObject.SetActive(false);
    }

    private void Update()
    {
        if (textFinished)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                textFinished = false;
                messageObject.SetActive(false);
                PlayerManager.Instance.Resume();
            }
        }
        else
        {
            if (TextWritter.isWritting && messageObject.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    textWritter.EndText();


                    pauseSettings?.ApplySettings();
                }
            }

        }

    }

    public void WriteMessage(string message)
    {
        if (pauseSettings == null)
        {
            pauseSettings = new PauseSettings();
        }

        pauseSettings.SaveSettings();

        messageText.text = "";
        messageObject.SetActive(true);

        textWritter.myText = messageText;
        textWritter.WriteText(message);
        PlayerManager.Instance.Pause();

        StartCoroutine(WaitUntilWrittingEnd());
    }

    IEnumerator WaitUntilWrittingEnd()
    {
        yield return new WaitUntil(() => !TextWritter.isWritting);
        OnWrittingEnd();
    }

    private void OnWrittingEnd()
    {
        textFinished = true;
    }

}
