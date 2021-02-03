using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartButton : MonoBehaviour
{
    
    goddesEgg egg;

    
    // public void RestartGame() {
    //     if(egg.ded == true )
    //     SceneManager.LoadScene("Main");
    // }
    void Update() {
        if(egg.ded == true) {
            SceneManager.LoadScene("Main");
        }
    }
}
