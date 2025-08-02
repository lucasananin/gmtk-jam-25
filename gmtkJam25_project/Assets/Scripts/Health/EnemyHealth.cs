using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBehaviour
{
    public static event System.Action<HealthBehaviour> OnAnyAiDead = null;

    protected override void OnDead_()
    {
        base.OnDead_();
        OnAnyAiDead?.Invoke(this);
        gameObject.SetActive(false);
        //Debug.Log($"a");
    }
}
