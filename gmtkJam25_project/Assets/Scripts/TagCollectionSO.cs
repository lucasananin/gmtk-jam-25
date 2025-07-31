using UnityEngine;

[CreateAssetMenu(fileName = "Tags_", menuName = "SO/Tag Collection")]
public class TagCollectionSO : ScriptableObject
{
    [SerializeField] string[] _list = null;

    public string[] List { get => _list; private set => _list = value; }
}
