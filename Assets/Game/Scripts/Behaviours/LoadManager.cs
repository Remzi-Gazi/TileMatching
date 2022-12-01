using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _loadingProgressText;
    
    [Header("Game Scenes")]
    [SerializeField] private GameScene _initializationScene;
    [SerializeField] private GameScene _mainMenuScene;
    [SerializeField] private GameScene _levelScene;
   

    private AsyncOperation _loadingOperation;

    private void Start()
    {
        LoadScene(_mainMenuScene, true);
        SceneManager.UnloadSceneAsync(_initializationScene.scenePath);
    }
    public void LoadLevelScene()
    {
        LoadScene(_levelScene, true);
        SceneManager.UnloadSceneAsync(_mainMenuScene.scenePath); 
    }

    private void LoadScene(GameScene scene, bool showLoadingScreen)
    {
        _loadingOperation = SceneManager.LoadSceneAsync(scene.scenePath, LoadSceneMode.Additive);
        if (showLoadingScreen)
        {
            StartCoroutine(ShowLoadingProgress());
        }
        
    }
 
    private IEnumerator ShowLoadingProgress()
    {
        if (_loadingOperation != null)
        {
            while (!_loadingOperation.isDone)
            {
                float loadingProgress = _loadingOperation.progress;
                _loadingProgressText.text = "LOADING "+(int)(loadingProgress*100) + " %";
                _loadingProgressText.enabled = true;
                yield return null;
            }
            if (_loadingOperation.isDone)
            {
                _loadingProgressText.enabled = false;
            }
            
        }
    }

}
