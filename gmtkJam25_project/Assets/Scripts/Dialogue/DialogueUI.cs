using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : CanvasView
{
    [Header("// DIALOGUE")]
    [SerializeField] TextMeshProUGUI _nameText = null;
    [SerializeField] TextMeshProUGUI _messageText = null;
    [SerializeField] Button _continueButton = null;
    [SerializeField] float _textSpeed = 0.05f;

    [Header("// READONLY")]
    [SerializeField] DialogueSO _currentSO = null;
    [SerializeField] int _currentIndex = 0;
    [SerializeField] bool _isTyping = false;

    [Header("// DEBUG")]
    [SerializeField] DialogueSO _debugSO = null;

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(Continue);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(Continue);
    }

    //protected override void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        PlayDebug();
    //    }
    //    base.Update();
    //}

    [ContextMenu("PlayDebug()")]
    public void PlayDebug()
    {
        Play(_debugSO);
    }

    public void Play(DialogueSO _so)
    {
        _currentSO = _so;
        _currentIndex = 0;
        Show();
        StopAllCoroutines();
        StartCoroutine(Play_Routine(_so.List[_currentIndex]));
    }

    private IEnumerator Play_Routine(DialogueData _data)
    {
        _isTyping = true;
        _nameText.text = _data.character.ToString();
        _messageText.text = string.Empty;

        while (IsShowing()) yield return null;

        var _textLength = _data.text.Length;

        for (int i = 0; i < _textLength; i++)
        {
            char item = _data.text[i];
            _messageText.text += item;
            yield return new WaitForSeconds(_textSpeed);
        }

        _isTyping = false;
    }

    private void Continue()
    {
        if (_isTyping)
        {
            StopAllCoroutines();
            _messageText.text = _currentSO.List[_currentIndex].text;
            _isTyping = false;
        }
        else
        {
            if (_currentIndex >= _currentSO.List.Length - 1)
            {
                Hide();
            }
            else
            {
                _currentIndex++;
                StartCoroutine(Play_Routine(_currentSO.List[_currentIndex]));
            }
        }
    }
}
