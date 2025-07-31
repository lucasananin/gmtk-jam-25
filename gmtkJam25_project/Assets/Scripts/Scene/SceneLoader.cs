using System.Collections;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string _sceneToLoad = null;
    [SerializeField] string _sceneToUnload = null;
    [SerializeField] bool _loadOnStart = false;

    private SceneHandler _manager = null;

    private void OnValidate()
    {
        TrySetReferences();
    }

    private IEnumerator Start()
    {
        yield return null;

        if (_loadOnStart)
        {
            Load();
        }
    }

    public void Load()
    {
        TrySetReferences();
        _manager.LoadScene(_sceneToLoad, _sceneToUnload);
    }

    private void TrySetReferences()
    {
        if (_manager == null)
        {
            _manager = FindFirstObjectByType<SceneHandler>();
        }
    }
}
