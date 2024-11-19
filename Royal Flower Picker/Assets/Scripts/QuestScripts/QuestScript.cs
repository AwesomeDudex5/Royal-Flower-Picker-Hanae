using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class QuestScript : MonoBehaviour
{
    public int requestID;
    public TextAsset beforeStoryINKfile;
    public TextAsset afterStoryINKfile;
    private StoryProcessor sp;

    // Start is called before the first frame update
    void Start()
    {
        sp = FindObjectOfType<StoryProcessor>();
        GameManager.current.onStartRequest += startRequest;
        GameManager.current.onCloseRequest += endRequest;
    }


    void startRequest(int questID)
    {
        if (questID == requestID)
        {
            sp.inkFile = beforeStoryINKfile;
            GameManager.current.onStartRequest -= startRequest;
            GameManager.current.dialogueStart();
        }
    }

    void endRequest(int questID)
    {
        if (questID == requestID)
        {
            sp.inkFile = afterStoryINKfile;
            GameManager.current.onCloseRequest -= endRequest;
            GameManager.current.dialogueStart();
        }
    }



}
