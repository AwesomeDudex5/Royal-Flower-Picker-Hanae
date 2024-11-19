using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequestorScript : MonoBehaviour, IInteractable
{
    public int requestID;
    public TextMeshPro requestPrompt;
    public flowerType ft;
    public int amount;

    [Header("Request Text")]
    public string askingText;
    public string requestText;
    public string notEnoughText;
    public string thankyouText;

    private BoxCollider2D bc;
    private bool canAccept;
    private bool requestFulfilled;
    private bool requestStarted;
    private bool canInteract;

    // Start is called before the first frame update
    void Start()
    {
        requestPrompt.text = askingText;
        requestPrompt.gameObject.SetActive(false);
        canAccept = false;
        requestFulfilled = false;
        requestStarted = false;
        canAccept = true;
        canInteract = true;
        GameManager.current.onUpdateRequest += updateRequest;
        GameManager.current.onDialogueStart += disableInteract;
        GameManager.current.onDialogueEnd += enableInteract;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (canInteract == true)
            {
                requestPrompt.gameObject.SetActive(true);
                GameManager.current.triggerInteractPrompt(true);
            }
            if (canInteract == false)
                requestPrompt.gameObject.SetActive(false);

            if (requestFulfilled == false)
            {
                canAccept = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (canInteract == true)
                requestPrompt.gameObject.SetActive(true);
            if(canInteract == false)
                requestPrompt.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            requestPrompt.gameObject.SetActive(false);
            if (requestFulfilled == false)
            {
                canAccept = false;
            }

            requestPrompt.text = requestText;
            GameManager.current.triggerInteractPrompt(false);

        }
    }

    public void interact()
    {
        if (canAccept == true)
        {
            if (requestFulfilled == false && requestStarted == true)
                GameManager.current.FlowerGiven(ft, amount, requestID);
            if (requestStarted == false)
            {
                requestStarted = true;
                requestPrompt.text = requestText;
                GameManager.current.StartRequest(requestID);
            }

        }
    }

    void updateRequest(bool isCompleted, int questID)
    {
        if (requestID == questID)
        {
            if (isCompleted == true)
            {
                requestPrompt.text = thankyouText;
                requestFulfilled = true;
                GameManager.current.CloseRequest(requestID);
                GameManager.current.triggerInteractPrompt(false);
                Destroy(GetComponent<RequestorScript>());
            }
            else
            {
                requestPrompt.text = notEnoughText;
                requestFulfilled = false;
            }
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
