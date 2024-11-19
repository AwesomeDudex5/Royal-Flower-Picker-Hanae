using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private GameObject go;
    public bool canInteract;

    private void Awake()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canInteract = true;
        GameManager.current.onDialogueStart += disableInteract;
        GameManager.current.onDialogueEnd += enableInteract;

        GameManager.current.onToggleUIOn += disableInteract;
        GameManager.current.onToggleUIOff += enableInteract;
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (go != null && go.GetComponent<IInteractable>() != null)
                {
                    AudioM.instance.playSound("Interact Sound");
                    go.GetComponent<IInteractable>().interact();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Interactable")
        {
            canInteract = true;

            // GameManager.current.triggerInteractPrompt(canInteract);
            go = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Interactable")
        {
            canInteract = false;
            // GameManager.current.triggerInteractPrompt(canInteract);
            go = null;
        }
    }


    void enableInteract()
    {
        canInteract = true;
    }

    void disableInteract()
    {
        canInteract = false;
    }
}
