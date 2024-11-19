using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public Text textName;
    public Text textDialogue;
    public Image spritePlace1;
    public Image spritePlace2;
    public Sprite defaultSprite;
    private Color grayedOut = new Color(0.5f, 0.5f, 0.5f);
    private CharacterSpriteManager cm;

    // Start is called before the first frame update
    void Start()
    {
        cm = FindObjectOfType<CharacterSpriteManager>();
        dialogueCanvas.SetActive(false);
        spritePlace1.sprite = defaultSprite;
        spritePlace2.sprite = defaultSprite;

        GameManager.current.onSetSprite += displaySprites;
        GameManager.current.onSetDialogue += processDialogue;
        GameManager.current.onDialogueStart += setupCanvas;
        GameManager.current.onDialogueEnd += disableCanvas;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setupCanvas()
    {
        dialogueCanvas.SetActive(true);
      //  GameManager.current.onTriggerInteractPrompt(false);
    }

    public void disableCanvas()
    {
        spritePlace1.sprite = defaultSprite;
        spritePlace2.sprite = defaultSprite;
        dialogueCanvas.SetActive(false);
        //GameManager.current.onTriggerInteractPrompt(true);
    }

    public void processDialogue(string charName, string dialogue)
    {
        textName.text = charName;
        textDialogue.text = dialogue;
    }

    public void displaySprites(string charName, string spriteName)
    {
        //spritePlace.sprite = cm.getSprite(charName, spriteName);
        if (charName.ToLower() == "hanae")
        {
            spritePlace1.sprite = cm.getSprite(charName, spriteName);
            spritePlace1.color = Color.white;
            spritePlace2.color = grayedOut;
        }
        else
        {
            spritePlace2.sprite = cm.getSprite(charName, spriteName);
            spritePlace2.color = Color.white;
            spritePlace1.color = grayedOut;
        }
    }

    public void resetCanvas()
    {
        spritePlace1.sprite = null;
        spritePlace2.sprite = null;
    }
}
