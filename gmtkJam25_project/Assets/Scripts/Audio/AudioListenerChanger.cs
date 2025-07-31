using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioListenerChanger : MonoBehaviour
{
    [SerializeField] AudioListener _listener = null;

    private void OnValidate()
    {
        _listener = GetComponent<AudioListener>();
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        var _listeners = FindObjectsByType<AudioListener>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
        _listener.enabled = _listeners.Length < 2;
    }
}
