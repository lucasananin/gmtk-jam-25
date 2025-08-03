using UnityEngine;

public class CaveDialogue : DialoguePlayer
{
    [Header("// CAVE REFERENCES")]
    [SerializeField] DialogueSO _initialDialogue = null;
    [SerializeField] DialogueSO _winDialogue = null;
    [SerializeField] DialogueSO _loseDialogue = null;

    private void Start()
    {
        PlayThis(_initialDialogue);
    }

    public override void PlayWin()
    {
        PlayThis(_winDialogue);
    }

    public override void PlayLose()
    {
        PlayThis(_loseDialogue);
    }
}
