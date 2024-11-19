using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator sceneAnim;
    public GameObject nextDayText;
    private int currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        nextDayText.SetActive(false);
        GameManager.current.onEndDay += triggerLoadScene;
    }


    public void triggerLoadScene()
    {
        StartCoroutine(loadNextScene());
    }

    IEnumerator loadNextScene()
    {
        GameManager.current.onEndDay -= triggerLoadScene;
        sceneAnim.SetTrigger("LoadScene");
        yield return new WaitForSeconds(2f);
        nextDayText.SetActive(true);
        yield return new WaitForSeconds(3f);
        currentScene++;
        if (currentScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentScene);
        }
        else
        {
            //currentScene = 0;
            SceneManager.LoadScene(0);
        }
    }

    public void loadSpecificScene(int index)
    {
        Time.timeScale = 1;
        StartCoroutine(loadScene(index));
    }

    IEnumerator loadScene(int index)
    {
        sceneAnim.SetTrigger("LoadScene");
        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene(index);

    }
}
