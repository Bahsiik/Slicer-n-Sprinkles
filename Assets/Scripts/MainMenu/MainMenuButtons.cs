using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuButtons : MonoBehaviour
    {
        public void GotToDifficultyMenu()
        {
            SceneManager.LoadScene("DifficultySelectionMenu");
        }
        
        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
