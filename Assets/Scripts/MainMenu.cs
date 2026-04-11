
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Lance la scène suivante du jeu
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Ferme l'application
    public void QuitGame()
    {
        Debug.Log("Le jeu se ferme !");
        Application.Quit();
    }
}