using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class TextWritter : MonoBehaviour
{
    public static bool isWritting = false;

    public float delay = 0.1f;
    public string textToWrite;
    public TextMeshProUGUI myText;
    public AudioClip characterSound;

    private Coroutine writter;
    private AudioSource audioSource;

    private void Start()
    {
        isWritting = false;
        audioSource = GetComponent<AudioSource>();
    }

    

    public void EndText()
    {
        StopCoroutine(writter);
        writter = null;
        myText.text = textToWrite;
        isWritting = false;
    }

    public void WriteText(string text)
    {
        if (isWritting) return;
        textToWrite = text;
        writter = StartCoroutine(Write());
    }

    public void WriteText()
    {
        if (isWritting) return;
        writter = StartCoroutine(Write());
    }

    IEnumerator Write()
    {
        myText.text = "";
        foreach (char c in textToWrite)
        {
            isWritting = true;
            myText.text += c;
            if(characterSound != null)
            {
                //audioSource.PlayOneShot(characterSound);
            }
            yield return new WaitForSeconds(delay);
        }
        EndText();
    }

}
