using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseView : CanvasView
{
    [SerializeField] Button _resumeButton = null;

    [Header("// READONLY")]
    [SerializeField] bool _isPaused = false;

    public static event UnityAction<bool> OnPauseChanged = null;

    private void Awake()
    {
        InstantHide();
    }

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(TogglePause);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(TogglePause);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0;
            Show();
            OnPauseChanged?.Invoke(true);
        }
        else
        {
            Time.timeScale = 1;
            Hide();
            OnPauseChanged?.Invoke(false);
        }
    }
}
