using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Outside");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}