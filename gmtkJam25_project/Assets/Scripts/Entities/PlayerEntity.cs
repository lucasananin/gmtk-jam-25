using UnityEngine;

public class PlayerEntity : EntityBehaviour
{
    //[SerializeField] PlayerMover _mover = null;
    [SerializeField] InputHandler _input = null;

    //private void OnValidate()
    //{
    //    if (_mover == null)
    //        _mover = GetComponent<PlayerMover>();
    //}

    //private void OnEnable()
    //{
    //    EnemySpawner.OnEndWaveGroupChanged += ResetPosition;
    //}

    //private void OnDisable()
    //{
    //    EnemySpawner.OnEndWaveGroupChanged -= ResetPosition;
    //}

    public override bool IsMoving()
    {
        return _input.Move != Vector2.zero;
    }

    //private void ResetPosition(WaveSO _waveSo)
    //{
    //    transform.position = Vector3.zero;
    //}
}
