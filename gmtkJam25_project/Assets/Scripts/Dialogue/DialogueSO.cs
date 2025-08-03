using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue_", menuName = "Scriptable Objects/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    [SerializeField] DialogueData[] _list = null;

    public DialogueData[] List { get => _list; }
}

[System.Serializable]
public class DialogueData
{
    public Character character = default;
    [TextArea(3, 12)] public string text = null;
}

public enum Character
{
    Pawnsley,
    Kinghub,
}