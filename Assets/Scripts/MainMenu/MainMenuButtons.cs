using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuButtons : MonoBehaviour
    {
        public void StartGameButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        public void QuitGameButton()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
