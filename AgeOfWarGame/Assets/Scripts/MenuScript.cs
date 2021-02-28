using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour {

    public void OnPlay() {
        SceneManager.LoadScene("Game");
    }

    public void OnExit() {
        Application.Quit();
    }
}
