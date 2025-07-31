using UnityEngine;
using UnityEngine.UI;

public class QuitAppButton : MonoBehaviour
{
    [SerializeField] Button _button = null;

    private void OnValidate()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(QuitApplication);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(QuitApplication);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
