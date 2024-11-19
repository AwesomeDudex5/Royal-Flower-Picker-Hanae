using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class EndOfDayTextScript : MonoBehaviour
{
    public TextAsset endOfDayText;
    private StoryProcessor sp;

    // Start is called before the first frame update
    void Start()
    {
        sp = GameObject.FindObjectOfType<StoryProcessor>();
        GameManager.current.onDialogueEnd += endTheDay;
    }


    void endTheDay()
    {
        if (GameManager.current.totalRequests <= GameManager.current.requestsDone)
        {
            StartCoroutine(waitAFew());
        }
    }

    IEnumerator waitAFew()
    {
        yield return new WaitForSeconds(0.98f);
        sp.inkFile = endOfDayText;
        GameManager.current.dialogueStart();
        GameManager.current.onDialogueEnd -= endTheDay;
    }
}
