using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicHandler : MonoBehaviour
{
    public GameObject[] comicPages;
    private bool isLoadingComic = false;
    int comicIndex = 0;
    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //disable comic pages
        for(int i=0; i<comicPages.Length;i++)
        {
            comicPages[i].SetActive(false);
        }
        //comicPages[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (comicIndex < comicPages.Length)
            {
                if (isLoadingComic == false)
                    loadComic();

            }
            else
            {
                //SceneManager.LoadScene(currentSceneIndex++);
                for (int i = 0; i < comicPages.Length; i++)
                {
                    comicPages[i].SetActive(false);
                }
                GameManager.current.endDay();
            }
        }
    }

    void loadComic()
    {
        isLoadingComic = true;

        comicPages[comicIndex].SetActive(true);

        if (comicIndex - 1 >= 0)
        {
            comicPages[comicIndex - 1].SetActive(false);
        }
       
        comicIndex++;
        //comicPages[comicIndex-1].SetActive(false);

        isLoadingComic = false;
    }

    IEnumerator loadNextScene()
    {
        for (int i = 0; i < comicPages.Length; i++)
        {
            comicPages[i].SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(currentSceneIndex++);
    }
}

