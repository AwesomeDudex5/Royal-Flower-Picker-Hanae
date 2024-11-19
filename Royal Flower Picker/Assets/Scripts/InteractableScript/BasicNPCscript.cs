using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicNPCscript : MonoBehaviour, IInteractable
{
    public TextAsset baseStoryINKfile;
    public TextMeshPro requestPrompt;

    private StoryProcessor sp;
    private bool canInteract;

    // Start is called before the first frame update
    void Start()
    {
        sp = FindObjectOfType<StoryProcessor>();
        requestPrompt.gameObject.SetActive(true);
        GameManager.current.onDialogueStart += disableInteract;
        GameManager.current.onDialogueEnd += enableInteract;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            canInteract = true;
            GameManager.current.triggerInteractPrompt(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            canInteract = false;
            GameManager.current.triggerInteractPrompt(false);
        }
    }

    public void interact()
    {
        if (canInteract == true)
        {
            sp.inkFile = baseStoryINKfile;
            GameManager.current.dialogueStart();
        }
    }

    
    void disableInteract()
    {
        canInteract = false;
        requestPrompt.gameObject.SetActive(false);
    }
    void enableInteract()
    {
        canInteract = true;
        requestPrompt.gameObject.SetActive(true);
    }
    
}
