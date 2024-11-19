using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject InteractPromptUI;
    public GameObject flowerInventoryUI;
    public GameObject pauseMenuUI;
    public bool UITOGGLE;
    public bool pauseToggle;

    public Text yellowFlowerCount;
    public Text pinkFlowerCount;
    public Text redFlowerCount;
    public Text whiteFlowerCount;
    public Text blueFlowerCount;
    public Text purpleFlowerCount;

    private int yellowCurrent;
    private int pinkCurrent;
    private int redCurrent;
    private int whiteCurrent;
    private int blueCurrent;
    private int purpleCurrent;


    // Start is called before the first frame update
    void Start()
    {
        flowerInventoryUI.SetActive(false);
        InteractPromptUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        UITOGGLE = false;
        pauseToggle = false;
        updateFlowerCount();
        GameManager.current.onUpdateUI += updateFlowerCount;
        GameManager.current.onTriggerInteractPrompt += toggleInteractPrompt;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (UITOGGLE == true)
            {
                AudioM.instance.playSound("Inventory Sound");
                UITOGGLE = false;
                flowerInventoryUI.SetActive(false);
                GameManager.current.toggleUIOFF();
            }
            else if(UITOGGLE == false)
            {
                AudioM.instance.playSound("Inventory Sound");
                UITOGGLE = true;
                updateFlowerUIText();
                flowerInventoryUI.SetActive(true);
                GameManager.current.toggleUION();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseToggle == true)
            {
                pauseToggle = false;
                disablePause();
            }
            else if(pauseToggle == false)
            {
                pauseToggle = true;
                enablePause();
            }
        }
    }

    void updateFlowerCount()
    {
        yellowCurrent = GameManager.current.numYellowFlowers;
        pinkCurrent = GameManager.current.numPinkFlowers;
        redCurrent = GameManager.current.numRedFlowers;
        whiteCurrent = GameManager.current.numWhiteFlowers;
        blueCurrent = GameManager.current.numBlueFlowers;
        purpleCurrent = GameManager.current.numPurpleFlowers;
    }

    //need code to update flower UI text

    void updateFlowerUIText()
    {
        yellowFlowerCount.text = " x " + yellowCurrent;
        redFlowerCount.text = " x " + redCurrent;
        pinkFlowerCount.text = " x " + pinkCurrent;
        whiteFlowerCount.text = " x " + whiteCurrent;
        blueFlowerCount.text = " x " + blueCurrent;
        purpleFlowerCount.text = " x " + purpleCurrent;
    }

   void toggleInteractPrompt(bool toggle)
    {
        InteractPromptUI.SetActive(toggle);
    }

   public void enablePause()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }

    public void disablePause()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
    }
}
