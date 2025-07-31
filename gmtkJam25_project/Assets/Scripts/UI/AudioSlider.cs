using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] Slider _slider = null;
    [SerializeField] Image _icon = null;
    [SerializeField] Sprite _onIcon = null;
    [SerializeField] Sprite _offIcon = null;

    private void Start()
    {
        _slider.value = AudioListener.volume;
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(ChangeVolume);
    }

    private void ChangeVolume(float _value)
    {
        AudioListener.volume = _value;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        _icon.sprite = AudioListener.volume > 0 ? _onIcon : _offIcon;
    }
}
