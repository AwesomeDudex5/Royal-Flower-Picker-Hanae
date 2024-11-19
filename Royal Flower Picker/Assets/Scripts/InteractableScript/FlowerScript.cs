using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum flowerType { pinkFlower, redFlower, blueFlower, purpleFlower, yellowFlower, whiteFlower }

public class FlowerScript : MonoBehaviour, IInteractable
{
    public flowerType _flowerType;
    public float spawnTime;
    public bool isWatered;
    private GameObject flowerSpriteObject;
    private GameObject darkSoilObject;
    [HideInInspector] public bool canPick;
    [HideInInspector] public bool playerIsInRange;

    // Start is called before the first frame update
    void Start()
    {
        flowerSpriteObject = this.transform.GetChild(0).gameObject;
        darkSoilObject = this.transform.GetChild(1).gameObject;
        playerIsInRange = false;
        if (isWatered == false)
        {
            canPick = false;
            flowerSpriteObject.SetActive(false);
            darkSoilObject.SetActive(true);
        }
        else
        {
            canPick = true;
            flowerSpriteObject.SetActive(true);
            darkSoilObject.SetActive(false);
        }
    }

    public void interact()
    {
        if (canPick)
        {
            StartCoroutine(disableFlower());
            GameManager.current.FlowerPicked(_flowerType);
        }
        if (isWatered == false)
        {
            isWatered = true;
            canPick = true;
            flowerSpriteObject.SetActive(true);
            darkSoilObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIsInRange = true;

            if (isWatered == false)
            {
                GameManager.current.triggerInteractPrompt(true);
            }
            else if (isWatered == true)
            {
                if(canPick == true)
                    GameManager.current.triggerInteractPrompt(true);
                else
                    GameManager.current.triggerInteractPrompt(false);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIsInRange = false;
            GameManager.current.triggerInteractPrompt(false);
        }
    }

    IEnumerator disableFlower()
    {
        canPick = false;
        GameManager.current.onTriggerInteractPrompt(canPick);
        flowerSpriteObject.SetActive(false);
        this.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(spawnTime);


        flowerSpriteObject.SetActive(true);
        canPick = true;
        this.GetComponent<BoxCollider2D>().enabled = true;

        if (playerIsInRange == true)
            GameManager.current.onTriggerInteractPrompt(canPick);
    }
}
