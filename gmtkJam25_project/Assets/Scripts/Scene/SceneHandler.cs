using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] float _loadTimeOffset = 0.5f;

    private WaitForSeconds _waitTime = null;

    public static event Action OnStartLoading = null;
    public static event Action OnEndLoading = null;

    private void Awake()
    {
        _waitTime = new WaitForSeconds(_loadTimeOffset);
    }

    public void LoadScene(string _sceneToLoad, string _sceneToUnload)
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        StartCoroutine(LoadYourAsyncScene(_sceneToLoad, _sceneToUnload));
    }

    private IEnumerator LoadYourAsyncScene(string _sceneToLoad, string _sceneToUnload)
    {
        Time.timeScale = 1;
        OnStartLoading?.Invoke();
        yield return _waitTime;

        if (!string.IsNullOrEmpty(_sceneToUnload))
        {
            var _asyncUnload = SceneManager.UnloadSceneAsync(_sceneToUnload);
            yield return _asyncUnload;
        }

        var _asyncLoad = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);
        yield return _asyncLoad;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneToLoad));

        yield return _waitTime;
        OnEndLoading?.Invoke();
    }
}
