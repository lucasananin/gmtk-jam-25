using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicTrack : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip startClip;
    private AudioClip loopClip;

    public AudioClip DialogueStart;
    public AudioClip DialogueLoop;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SetClips(DialogueStart, DialogueLoop);
    }

    public void SetClips(AudioClip start, AudioClip loop)
    {
        startClip = start;
        loopClip = loop;
        PlayStartThenLoop();
    }

    void PlayStartThenLoop()
    {
        if (startClip != null)
        {
            audioSource.clip = startClip;
            audioSource.loop = false;
            audioSource.Play();
            Invoke(nameof(PlayLoop), startClip.length);
        }
        else
        {
            PlayLoop();
        }
    }

    void PlayLoop()
    {
        if (loopClip == null) return;

        audioSource.clip = loopClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
