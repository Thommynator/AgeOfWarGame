using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject gameCanvas;
    public GameObject pauseMenuCanvas;

    private bool isPaused;

    void Start() {
        this.isPaused = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Continue();
            } else {
                Pause();
            }
        }
    }

    public void Restart() {
        SceneManager.LoadScene("Game");
        this.Continue();
    }

    public void Pause() {
        this.isPaused = true;
        Time.timeScale = 0;
        gameCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);

    }

    public void Continue() {
        this.isPaused = false;
        Time.timeScale = 1;
        gameCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);

    }

    public void Exit() {
        Application.Quit();
    }
}
