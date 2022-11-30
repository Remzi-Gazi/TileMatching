using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    [SerializeField] private GameScene _persistentManagersScene;
    
    void Start()
    {
        SceneManager.LoadSceneAsync(_persistentManagersScene.scenePath, LoadSceneMode.Additive);
    }
}
