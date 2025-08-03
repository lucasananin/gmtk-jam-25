using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    [Header("// GENERAL")]
    [SerializeField] DialogueUI _dialogueUi = null;
    [SerializeField] PlayerHealth _playerHealth = null;
    [SerializeField] EnemyHealth _enemyHealth = null;

    //private void Awake()
    //{
    //    _dialogueUi = FindFirstObjectByType<DialogueUI>();
    //}

    private void OnEnable()
    {
        _playerHealth.OnDead += PlayLose;
        _enemyHealth.OnDead += PlayWin;
    }

    public virtual void PlayWin()
    {
    }

    public virtual void PlayLose()
    {
    }

    public void PlayThis(DialogueSO _so)
    {
        _dialogueUi.Play(_so);
    }
}
