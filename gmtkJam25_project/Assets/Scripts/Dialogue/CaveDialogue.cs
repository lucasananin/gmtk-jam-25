using UnityEngine;

public class CaveDialogue : DialoguePlayer
{
    [Header("// CAVE REFERENCES")]
    [SerializeField] DialogueSO _initialDialogue = null;
    [SerializeField] DialogueSO _winDialogue = null;
    [SerializeField] DialogueSO _loseDialogue = null;

    [Header("// CHARACTER SCRIPTS")]
    [SerializeField] InputHandler _input = null;
    [SerializeField] Bear _bear = null;

    private void Start()
    {
        PlayThis(_initialDialogue);
        _input.enabled = false;
        _input.enabled = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        DialogueUI.OnDialogueEnd += Proceed;
    }

    private void Proceed(DialogueSO _so)
    {

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
