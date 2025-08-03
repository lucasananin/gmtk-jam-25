using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    [Header("// GENERAL")]
    [SerializeField] protected DialogueUI _dialogueUi = null;
    [SerializeField] protected PlayerHealth _playerHealth = null;
    [SerializeField] protected EnemyHealth _enemyHealth = null;

    protected virtual void OnEnable()
    {
        _playerHealth.OnDead += PlayLose;
        _enemyHealth.OnDead += PlayWin;
    }

    protected virtual void OnDisable()
    {
        _playerHealth.OnDead -= PlayLose;
        _enemyHealth.OnDead -= PlayWin;
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
