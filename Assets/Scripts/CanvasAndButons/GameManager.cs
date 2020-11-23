using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject loadingScene;

    private void Awake()
    {
        instance = this;

        SceneManager.LoadSceneAsync((int)SceneIndex.TITLE_SCREEN, LoadSceneMode.Additive);

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGame()
    {
        loadingScene.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync((int)SceneIndex.TITLE_SCREEN);
        SceneManager.LoadSceneAsync((int)SceneIndex.MAINSCENE, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync((int)SceneIndex.MAINSCENE, LoadSceneMode.Additive);

    }
}
