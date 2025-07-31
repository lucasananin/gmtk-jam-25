using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMethods : MonoBehaviour
{
    public static bool HasAvailableTag<T>(GameObject _gameObjectHit, IReadOnlyList<T> _tags)
    {
        int _count = _tags.Count;

        for (int i = 0; i < _count; i++)
        {
            if (_gameObjectHit.CompareTag(_tags[i] as string))
            {
                return true;
            }
        }

        return false;
    }

    public static Vector3 GetRandomPointInBounds(Bounds _bounds)
    {
        return new Vector3(
            Random.Range(_bounds.min.x, _bounds.max.x),
            Random.Range(_bounds.min.y, _bounds.max.y),
            Random.Range(_bounds.min.z, _bounds.max.z)
        );
    }

    public static bool IsTheSameString(string _a, string _b)
    {
        return string.Equals(_a, _b, System.StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsPointCloseToTarget(Vector3 _point, Vector3 _target, float _minDistance)
    {
        float _distance = (_target - _point).sqrMagnitude;
        return _distance < _minDistance * _minDistance;
    }

    public static Vector2 GetRandomInCircle(float _min, float _max)
    {
        return Random.insideUnitCircle.normalized * Random.Range(_min, _max);
    }

    public static List<T> OrderListByDistance<T>(List<T> _list, Vector3 _origin) where T : Component
    {
        _list.Sort(delegate (T _a, T _b)
        {
            return (_a.transform.position - _origin).sqrMagnitude.CompareTo((_b.transform.position - _origin).sqrMagnitude);
        });

        return _list;
    }

    public static int CalculateAngle(Vector3 _targetPosition, Transform _sourceTransform)
    {
        Vector3 _targetDir = (_targetPosition - _sourceTransform.position).normalized;
        float _angle = Vector3.Angle(_targetDir, _sourceTransform.right);
        return (int)_angle;
    }

    public static Quaternion GetLookRotation(Vector3 _origin, Vector3 _target)
    {
        var _direction = (_target - _origin).normalized;
        float _angle2 = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        Quaternion _rotation = Quaternion.AngleAxis(_angle2, Vector3.forward);
        return _rotation;
    }

    public static Vector2 GetRandomDirection()
    {
        return Random.insideUnitCircle.normalized;
    }
}
