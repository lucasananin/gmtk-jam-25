using UnityEngine;
using System.Collections;

public class RobotManager : MonoBehaviour
{
    public FloatMotion[] bodyParts;
    public GameObject LeftArm;
    public GameObject RightArm;

    public void ControlFloating(bool canFloat)
    {
        foreach (var part in bodyParts)
            part.enabled = canFloat;
    }

    public void RaiseArms()
    {
        StopAllCoroutines();
        ControlFloating(false);
        StartCoroutine(RotateArm(RightArm.transform, 0f, 90f));
        StartCoroutine(RotateArm(LeftArm.transform, 0f, -90f));
    }

    public void LowerArms()
    {
        StopAllCoroutines();
        ControlFloating(false);
        StartCoroutine(RotateArm(RightArm.transform, RightArm.transform.localEulerAngles.z, 0f));
        StartCoroutine(RotateArm(LeftArm.transform, LeftArm.transform.localEulerAngles.z, 0f));
    }

    public IEnumerator RotateArm(Transform arm, float fromAngle, float toAngle, float duration = 0.5f)
    {
        float elapsed = 0f;
        fromAngle = NormalizeAngle(fromAngle);
        toAngle = NormalizeAngle(toAngle);

        while (elapsed < duration)
        {
            float z = Mathf.LerpAngle(fromAngle, toAngle, elapsed / duration);
            arm.localRotation = Quaternion.Euler(0f, 0f, z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        arm.localRotation = Quaternion.Euler(0f, 0f, toAngle);
        ControlFloating(true);
    }

    float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
