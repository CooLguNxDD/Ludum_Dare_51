using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string GameSceneName;
    public string MenuName;
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(MenuName);
    }

}
