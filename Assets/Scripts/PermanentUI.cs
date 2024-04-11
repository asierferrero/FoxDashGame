using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PermanentUI : MonoBehaviour
{
    // Player stats
    public int cherries = 0;
    public int health = 5;
    public int highScore = 0;
    public TextMeshProUGUI cherryText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI healthAmount;

    [SerializeField] private GameObject gameOverMenu;
    public static PermanentUI perm;

    private bool gameOverStarted = false;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        // Set cherry count
        cherryText.text = cherries.ToString();
        highScoreText.text = highScore.ToString();

        // Singleton
        if (!perm)
        {
            perm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Reset()
    {
        if (health != 0)
        {
            cherries = 0;
            health--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOverScene");
        }
        cherryText.text = cherries.ToString();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            cherries = 0; // Reset cherry count to 0 when the main menu scene is loaded
            cherryText.text = cherries.ToString();
        }
    }

    public void IncreaseCherries()
    {
        cherries++;
        cherryText.text = cherries.ToString();
        if (cherries > highScore)
        {
            highScore = cherries;
            highScoreText.text = highScore.ToString();
        }
    }

    public void HandleHealth()
    {
        health--;
        healthAmount.text = health.ToString();
        if (health <= 0 && !gameOverStarted)
        {
            gameOverStarted = true;
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
