using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour
{
    public RobotManager robotManager;
    public Transform arms;
    public Transform rightArmTarget;
    public Transform leftArmTarget;
    public Transform[] bulletPoints;
    public GameObject bulletPrefab;
    public float bulletDelay = 0.1f;
    public int revolutions = 3;
    public float rotationSpeed = 180f;

    public Head head;
    public bool attacc = false;
    public bool headAttacc = false;

    private Transform player;
    private bool isBulletHellRunning = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (attacc && !isBulletHellRunning)
        {
            attacc = false;
            ActivateBulletHell();
        }
        if(headAttacc)
        {
            headAttacc = false;
            ActivateLaserAttack(player, 6f, 10f, 3f);
        }
    }

    public void ActivateBulletHell()
    {
        revolutions = Random.Range(2, 5);
        StartCoroutine(BulletHellSequence());
    }

    IEnumerator BulletHellSequence()
    {
        isBulletHellRunning = true;
        robotManager.ControlFloating(false);
        robotManager.StopAllCoroutines();

        yield return StartCoroutine(robotManager.RotateArm(
            robotManager.RightArm.transform,
            robotManager.RightArm.transform.localEulerAngles.z,
            rightArmTarget.localEulerAngles.z));

        yield return StartCoroutine(robotManager.RotateArm(
            robotManager.LeftArm.transform,
            robotManager.LeftArm.transform.localEulerAngles.z,
            leftArmTarget.localEulerAngles.z));

        float totalAngle = 360f * revolutions;
        float rotated = 0f;
        float shootTimer = 0f;

        while (rotated < totalAngle)
        {
            float deltaAngle = rotationSpeed * Time.deltaTime;
            rotated += deltaAngle;
            arms.Rotate(0f, 0f, deltaAngle);

            shootTimer += Time.deltaTime;
            if (shootTimer >= bulletDelay)
            {
                foreach (Transform point in bulletPoints)
                {
                    Instantiate(bulletPrefab, point.position, point.rotation);
                }
                shootTimer = 0f;
            }

            yield return null;
        }

        robotManager.ControlFloating(true);
        isBulletHellRunning = false;
    }

    public void ActivateLaserAttack(Transform player, float speed, float returnDelay, float yOffset)
    {
        StartCoroutine(StartAndStopLaser(player, speed, returnDelay, yOffset));
    }

    IEnumerator StartAndStopLaser(Transform player, float speed, float returnDelay, float yOffset)
    {
        StartHeadLaser(player, speed, yOffset);

        yield return new WaitForSeconds(returnDelay);

        //StopHeadLaser();
    }

    void StartHeadLaser(Transform player, float speed, float yOffset)
    {
        if (head != null)
        {
            head.moveSpeed = speed;
            head.yOffset = yOffset;
            head.ActivateLaserTowards(player);
        }
    }
}
