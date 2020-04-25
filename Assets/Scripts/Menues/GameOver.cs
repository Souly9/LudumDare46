using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button m_startButton;

    public Button m_quitGame;

    public AudioSource m_audio;

    void Start()
    {
        m_startButton.onClick.AddListener(RestartGame);
        m_quitGame.onClick.AddListener(QuitGame);
    }
    public void RestartGame()
    {
        m_audio.Play();
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {
        m_audio.Play();
        Application.Quit();
    }
}


