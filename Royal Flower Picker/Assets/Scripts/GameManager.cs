using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    //Number of Flowers
    public int numPinkFlowers;
    public int numRedFlowers;
    public int numBlueFlowers;
    public int numPurpleFlowers;
    public int numYellowFlowers;
    public int numWhiteFlowers;

    [Header("Request Stats")]
    public int totalRequests;
    public int requestsDone;

    public void Awake()
    {
        if (current == null)
        {
            current = this;
        }

        numPinkFlowers = GameValues.currentPinkFlowers;
        numRedFlowers = GameValues.currentRedFlowers;
        numBlueFlowers = GameValues.currentBlueFlowers;
        numPurpleFlowers = GameValues.currentPurpleFlowers;
        numYellowFlowers = GameValues.currentYellowFlowers;
        numWhiteFlowers = GameValues.currentWhiteFlowers;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.current.onFlowerPicked += increaseFlowerInventory;
        GameManager.current.onFlowerGiven += decreaseFlowerInventory;
        GameManager.current.onUpdateRequest += incrementQuests;
        GameManager.current.onEndDay += setGameValues;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setGameValues()
    {
        GameValues.currentBlueFlowers = numBlueFlowers;
        GameValues.currentPinkFlowers = numPinkFlowers;
        GameValues.currentPurpleFlowers = numPurpleFlowers;
        GameValues.currentRedFlowers = numRedFlowers;
        GameValues.currentWhiteFlowers = numWhiteFlowers;
        GameValues.currentYellowFlowers = numYellowFlowers;

       // GameManager.current.onEndDay -= setGameValues;
    }

    void increaseFlowerInventory(flowerType type)
    {
        switch (type)
        {
            case flowerType.blueFlower:
                numBlueFlowers++;
                //Debug.Log(type + ": " + numBlueFlowers);
                break;
            case flowerType.pinkFlower:
                numPinkFlowers++;
                //  Debug.Log(type + ": " + numPinkFlowers);
                break;
            case flowerType.purpleFlower:
                numPurpleFlowers++;
                //  Debug.Log(type + ": " + numPurpleFlowers);
                break;
            case flowerType.redFlower:
                numRedFlowers++;
                // Debug.Log(type + ": " + numRedFlowers);
                break;
            case flowerType.whiteFlower:
                numWhiteFlowers++;
                // Debug.Log(type + ": " + numWhiteFlowers);
                break;
            case flowerType.yellowFlower:
                numYellowFlowers++;
                //Debug.Log(type + ": " + numYellowFlowers);
                break;
            default:
                Debug.Log("No Flowers Updated");
                break;
        }
        GameManager.current.updateUI();
    }

    void decreaseFlowerInventory(flowerType type, int amount, int questID)
    {
        switch (type)
        {
            case flowerType.blueFlower:
                if (checkIfHaveEnough(numBlueFlowers, amount) == true)
                {
                    numBlueFlowers -= amount;
                    GameManager.current.UpdateRequest(true, questID);
                }
                else
                {
                    GameManager.current.UpdateRequest(false, questID);
                }
                break;
            case flowerType.pinkFlower:
                if (checkIfHaveEnough(numPinkFlowers, amount) == true)
                {
                    numPinkFlowers -= amount;
                    GameManager.current.UpdateRequest(true, questID);
                }
                else
                {
                    GameManager.current.UpdateRequest(false, questID);
                }
                break;
            case flowerType.purpleFlower:
                if (checkIfHaveEnough(numPurpleFlowers, amount) == true)
                {
                    numPurpleFlowers -= amount;
                    GameManager.current.UpdateRequest(true, questID);
                }
                else
                {
                    GameManager.current.UpdateRequest(false, questID);
                }
                break;
            case flowerType.redFlower:
                if (checkIfHaveEnough(numRedFlowers, amount) == true)
                {
                    numRedFlowers -= amount;
                    GameManager.current.UpdateRequest(true, questID);
                }
                else
                {
                    GameManager.current.UpdateRequest(false, questID);
                }
                break;
            case flowerType.whiteFlower:
                if (checkIfHaveEnough(numWhiteFlowers, amount) == true)
                {
                    numWhiteFlowers -= amount;
                    GameManager.current.UpdateRequest(true, questID);
                }
                else
                {
                    GameManager.current.UpdateRequest(false, questID);
                }
                break;
            case flowerType.yellowFlower:
                if (checkIfHaveEnough(numYellowFlowers, amount) == true)
                {
                    numYellowFlowers -= amount;
                    GameManager.current.UpdateRequest(true, questID);
                }
                else
                {
                    GameManager.current.UpdateRequest(false, questID);
                }
                break;
            default:
                Debug.Log("No Flowers Given");
                break;
        }
        GameManager.current.updateUI();
    }

    bool checkIfHaveEnough(int amountHave, int amountNeeded)
    {
        if (amountHave < amountNeeded)
            return false;
        else
            return true;
    }

    void incrementQuests(bool isComplete, int questID)
    {
        if (isComplete)
            requestsDone++;
    }

    #region List of Actions

    public Action<flowerType> onFlowerPicked;
    public void FlowerPicked(flowerType ft)
    {
        if (onFlowerPicked != null)
            onFlowerPicked(ft);
    }

    public Action<flowerType, int, int> onFlowerGiven;
    public void FlowerGiven(flowerType ft, int amount, int questID)
    {
        if (onFlowerGiven != null)
        {
            onFlowerGiven(ft, amount, questID);
        }
    }

    public Action<int> onStartRequest;
    public void StartRequest(int questID)
    {
        if (onStartRequest != null)
            onStartRequest(questID);
    }

    public Action<bool, int> onUpdateRequest;
    public void UpdateRequest(bool isCompleted, int questID)
    {
        if (onUpdateRequest != null)
        {
            onUpdateRequest(isCompleted, questID);
        }
    }

    public Action<int> onCloseRequest;
    public void CloseRequest(int questID)
    {
        if (onCloseRequest != null)
            onCloseRequest(questID);
    }

    public Action<string, string> onSetSprite;
    public void SetSprite(string charName, string spriteName)
    {
        if (onSetSprite != null)
        {
            onSetSprite(charName, spriteName);
        }
    }

    public Action<string, string> onSetDialogue;
    public void SetDialogue(string charName, string dialogue)
    {
        if (onSetDialogue != null)
        {
            onSetDialogue(charName, dialogue);
        }
    }

    public Action onDialogueStart;
    public void dialogueStart()
    {
        if (onDialogueStart != null)
        {
            onDialogueStart();
        }
    }

    public Action onDialogueEnd;
    public void dialogueEnd()
    {
        if (onDialogueEnd != null)
        {
            onDialogueEnd();
        }
    }

    public Action onEndDay;
    public void endDay()
    {
        if (onEndDay != null)
        {
            onEndDay();
        }
    }

    public Action onUpdateUI;
    public void updateUI()
    {
        if(onUpdateUI != null)
        {
            onUpdateUI();
        }
    }

    public Action onToggleUIOn;
    public void toggleUION()
    {
        if (onToggleUIOn != null)
        {
            onToggleUIOn();
        }
    }

    public Action onToggleUIOff;
    public void toggleUIOFF()
    {
        if (onToggleUIOff != null)
        {
            onToggleUIOff();
        }
    }

    public Action<bool> onTriggerInteractPrompt;
    public void triggerInteractPrompt(bool toggle)
    {
        if(onTriggerInteractPrompt != null)
        {
            onTriggerInteractPrompt(toggle);
        }
    }

    #endregion
}
