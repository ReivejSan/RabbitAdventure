using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public void InGame() {
        SceneManager.LoadScene("ChooseStage");
    }

    public void About() {
        SceneManager.LoadScene("About");
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
    }

    public void Shop() {
        SceneManager.LoadScene("Shop");
    }

    public void Scene1() {
        SceneManager.LoadScene("Main");
    }

    public void Scene2() {
        SceneManager.LoadScene("Stage2");
    }

    public void Scene3() {
        SceneManager.LoadScene("Stage 3");
    }

    public void Exit() {
        Application.Quit();
    }
}
