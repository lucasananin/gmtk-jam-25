using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasView : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup = null;
    [SerializeField] float _fadeDuration = 0.3f;
    //[SerializeField] bool _isModal = false;

    private float _lastAlpha = 0;
    private float _targetAlpha = 0;
    private float _timer = 0;

    //public static event UnityAction<CanvasView> OnModalShow = null;
    //public static event UnityAction<CanvasView> OnModalHide = null;
    //public static event UnityAction<CanvasView> OnShow = null;
    //public static event UnityAction<CanvasView> OnHide = null;

    private void OnValidate()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void Update()
    {
        if (_canvasGroup.alpha == _targetAlpha) return;

        _timer += Time.unscaledDeltaTime * (1f / _fadeDuration);
        _canvasGroup.alpha = Mathf.Lerp(_lastAlpha, _targetAlpha, _timer);
    }

    public virtual void Show()
    {
        _timer = 0;
        _targetAlpha = 1;
        _lastAlpha = _canvasGroup.alpha;
        _canvasGroup.blocksRaycasts = true;

        //if (_isModal)
        //    OnModalShow?.Invoke(this);
    }

    public virtual void Hide()
    {
        _timer = 0;
        _targetAlpha = 0;
        _lastAlpha = _canvasGroup.alpha;
        _canvasGroup.blocksRaycasts = false;

        //if (_isModal)
        //    OnModalHide?.Invoke(this);
    }

    public void InstantShow()
    {
        _timer = 1;
        _targetAlpha = 1;
        _lastAlpha = 1;
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;

        //if (_isModal)
        //    OnModalShow?.Invoke(this);
    }

    public void InstantHide()
    {
        _timer = 0;
        _targetAlpha = 0;
        _lastAlpha = 0;
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;

        //if (_isModal)
        //    OnModalHide?.Invoke(this);
    }

    public bool IsVisible()
    {
        return _canvasGroup.alpha > 0;
    }
}
