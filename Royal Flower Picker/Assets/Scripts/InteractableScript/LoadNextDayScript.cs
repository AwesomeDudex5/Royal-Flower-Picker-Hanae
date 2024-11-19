using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class LoadNextDayScript : MonoBehaviour, IInteractable
{
    public GameObject endDayPrompt;
    public TextAsset notDoneYetText;
    private StoryProcessor sp;

    // Start is called before the first frame update
    void Start()
    {
        sp = GameObject.FindObjectOfType<StoryProcessor>();
        endDayPrompt.SetActive(false);
    }

    public void interact()
    {
        endDayPrompt.SetActive(false);
        if (GameManager.current.totalRequests <= GameManager.current.requestsDone)
        {
            GameManager.current.endDay();

        }
        else
        {
            sp.inkFile = notDoneYetText;
            GameManager.current.dialogueStart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            GameManager.current.triggerInteractPrompt(true);
            endDayPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            GameManager.current.triggerInteractPrompt(false);
            endDayPrompt.SetActive(false);
        }
    }
}
