using UnityEngine;

public abstract class EntitySO : ScriptableObject
{
    [SerializeField] protected TagCollectionSO _opponentTags = null;
    [SerializeField] protected TagCollectionSO _projectileHitTag = null;

    public TagCollectionSO OpponentTags { get => _opponentTags; private set => _opponentTags = value; }
    public TagCollectionSO ProjectileHitTags { get => _projectileHitTag; private set => _projectileHitTag = value; }
}
