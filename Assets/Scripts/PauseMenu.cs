using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject PausePanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause")) {
            if (isPaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }

        }
    }

    public void PauseGame() {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        isPaused = false;
    }
}
