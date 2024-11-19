using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class StoryProcessor : MonoBehaviour
{
    [HideInInspector]
    public TextAsset inkFile;

    private bool dialogueMode = false;
    static Story story;
    string nametag;
    string spriteTag;
    string message;
    List<string> tags;

    // Start is called before the first frame update
    void Start()
    {
        tags = new List<string>();
        GameManager.current.onDialogueStart += enableDialogueMode;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueMode == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Is there more to the story?
                if (story.canContinue)
                {
                    setMessage();
                    parseTags();
                    updateDialogue();
                }
                else
                {
                    disableDialogueMode();
                }
            }
        }
    }

    void setMessage()
    {
        message = story.Continue();
    }

    void parseTags()
    {
        tags = story.currentTags;

        foreach (string t in tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(' ')[1];

            switch (prefix.ToLower())
            {
                case "name":
                    nametag = param;
                    break;
                case "sprite":
                    spriteTag = param;
                    break;
            }
        }
    }

    void updateDialogue()
    {
        GameManager.current.SetDialogue(nametag, message);
        GameManager.current.SetSprite(nametag, spriteTag);
    }

    void enableDialogueMode()
    {
        story = new Story(inkFile.text);

        setMessage();
        parseTags();
        updateDialogue();

        dialogueMode = true;
    }

    void disableDialogueMode()
    {
        inkFile = null;
        dialogueMode = false;
        GameManager.current.dialogueEnd();
    }
}


