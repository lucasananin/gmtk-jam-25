using UnityEngine;

[CreateAssetMenu(fileName = "WinLoseStreakSO", menuName = "Scriptable Objects/WinLoseStreakSO")]
public class WinLoseStreakSO : ScriptableObject
{
    [SerializeField] bool _wonCave = false;
    [SerializeField] bool _wonMedieval = false;
    [SerializeField] bool _wonFuture = false;

    public bool WonCave { get => _wonCave; set => _wonCave = value; }
    public bool WonMedieval { get => _wonMedieval; set => _wonMedieval = value; }
    public bool WonFuture { get => _wonFuture; set => _wonFuture = value; }
}
