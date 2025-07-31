using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Audio_", menuName = "Scriptable Objects/AudioSO")]
public class AudioSO : ScriptableObject
{
    [Header("// Source Values")]
    [SerializeField] AudioClip _clip = null;
    [SerializeField] AudioMixerGroup _output = null;
    [SerializeField] bool _isMusic = false;

    [Header("// Ignores")]
    [SerializeField] bool _bypassEffects = false;
    [SerializeField] bool _bypassListenerEffects = false;
    [SerializeField] bool _bypassReverbZones = false;
    [SerializeField] bool _ignoreListenerVolume = false;
    [SerializeField] bool _ignoreListenerPause = false;
    [SerializeField] bool _loop = false;

    [Header("// Sound properties")]
    [SerializeField] PriorityLevel _priorityLevel = PriorityLevel.Standard;
    [SerializeField, Range(0f, 1f)] float _volume = 1f;
    [SerializeField, Range(-3f, 3f)] float _pitch = 1f;
    [SerializeField, Range(-1f, 1f)] float _panStereo = 0f;
    [SerializeField, Range(0f, 1.1f)] float _reverbZoneMix = 1f;
    [SerializeField, Range(0f, 1f)] float _time = 0f;

    [Header("// Spatialisation")]
    [SerializeField, Range(0f, 1f)] public float _spatialBlend = 1f;
    [SerializeField] AudioRolloffMode _rolloffMode = AudioRolloffMode.Logarithmic;
    [SerializeField, Range(0.01f, 5f)] float _minDistance = 1f;
    [SerializeField, Range(5f, 100f)] float _maxDistance = 50f;
    [SerializeField, Range(0f, 5f)] float _dopplerLevel = 1f;
    [SerializeField, Range(0, 360)] int _spread = 0;

    public static event AudioPlayAction OnPlay = null;

    public bool IsMusic { get => _isMusic; }
    public int Priority { get { return (int)_priorityLevel; } set { _priorityLevel = (PriorityLevel)value; } }

    public AudioSource Play(Vector3 _position)
    {
        return OnPlay?.Invoke(this, _position);
    }

    public void ApplyTo(AudioSource _audioSource)
    {
        _audioSource.clip = _clip;
        _audioSource.outputAudioMixerGroup = _output;

        _audioSource.bypassEffects = _bypassEffects;
        _audioSource.bypassListenerEffects = _bypassListenerEffects;
        _audioSource.bypassReverbZones = _bypassReverbZones;
        _audioSource.ignoreListenerVolume = _ignoreListenerVolume;
        _audioSource.ignoreListenerPause = _ignoreListenerPause;
        _audioSource.loop = _loop;

        _audioSource.priority = Priority;
        _audioSource.volume = _volume;
        _audioSource.pitch = _pitch;
        _audioSource.panStereo = _panStereo;
        _audioSource.reverbZoneMix = _reverbZoneMix;
        _audioSource.time = _time;

        _audioSource.spatialBlend = _spatialBlend;
        _audioSource.rolloffMode = _rolloffMode;
        _audioSource.minDistance = _minDistance;
        _audioSource.maxDistance = _maxDistance;
        _audioSource.spread = _spread;
        _audioSource.dopplerLevel = _dopplerLevel;
    }

    private enum PriorityLevel
    {
        Highest = 0,
        High = 64,
        Standard = 128,
        Low = 194,
        VeryLow = 256,
    }
}

public delegate AudioSource AudioPlayAction(AudioSO _audioSO, Vector3 _position);
public delegate bool AudioStopAction(AudioSource _source);
