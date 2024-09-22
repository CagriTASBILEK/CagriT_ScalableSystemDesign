using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
        void Start()
        {
            GameInitiate();
        }

        private void GameInitiate()
        {
            LevelManager.Instance.LevelInitiate();
        }

        public void GameRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
