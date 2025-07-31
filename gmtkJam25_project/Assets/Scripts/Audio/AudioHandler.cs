using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] AudioSource _prefab = null;
    [SerializeField] float _fadeDuration = 1f;

    [Header("// READONLY")]
    [SerializeField] List<AudioSource> _sources = null;

    private void Awake()
    {
        PopulateList();
    }

    private void OnEnable()
    {
        AudioSO.OnPlay += PlayAudio;
    }

    private void OnDisable()
    {
        AudioSO.OnPlay -= PlayAudio;
    }

    private AudioSource PlayAudio(AudioSO _so, Vector3 _position)
    {
        var _source = _so.IsMusic ? _sources[0] : GetAvailableSource();
        _so.ApplyTo(_source);
        _source.transform.position = _position;
        _source.Play();
        return _source;
    }

    public void StopMusic()
    {
        var _musicSource = _sources[0];
        _musicSource.DOFade(0, _fadeDuration);
    }

    public AudioSource GetAvailableSource()
    {
        int _count = _sources.Count;

        for (int i = 1; i < _count; i++)
        {
            if (!_sources[i].isPlaying)
            {
                return _sources[i];
            }
        }

        var _instance = Instantiate(_prefab, transform);
        _sources.Add(_instance);
        return _instance;
    }

    private void PopulateList()
    {
        for (int i = 0; i < 10; i++)
        {
            var _instance = Instantiate(_prefab, transform);
            _sources.Add(_instance);
        }
    }
}
