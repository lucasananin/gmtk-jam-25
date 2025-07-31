using UnityEngine;

public class LoadingPanel : CanvasView
{
    private void Start()
    {
        InstantShow();
    }

    private void OnEnable()
    {
        SceneHandler.OnStartLoading += Show;
        SceneHandler.OnEndLoading += Hide;
    }

    private void OnDisable()
    {
        SceneHandler.OnStartLoading -= Show;
        SceneHandler.OnEndLoading -= Hide;
    }
}
