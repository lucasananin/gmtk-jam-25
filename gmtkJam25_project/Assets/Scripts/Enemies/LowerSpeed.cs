using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerDamage : MonoBehaviour
{
    private PlayerMover PlayerMover;
    private int loweredSpeed = 3;
    public float duration = 3f;
    private float OGSpeed;

    private void Start()
    {
        PlayerMover = GetComponent<PlayerMover>();
        OGSpeed = PlayerMover._moveSpeed;
    }

    public void LowerSpeed()
    {
        StartCoroutine(LoweredSpeed());
    }

    private IEnumerator LoweredSpeed()
    {
        PlayerMover._moveSpeed = loweredSpeed;

        yield return new WaitForSeconds(duration);

        PlayerMover._moveSpeed = OGSpeed;
    }

}
