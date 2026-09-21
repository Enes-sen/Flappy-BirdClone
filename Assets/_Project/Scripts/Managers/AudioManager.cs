using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource MSource;
    [SerializeField] private AudioClip[] Clipers;

    public static AudioManager Instance { get; private set; }
    public static AudioClip[] AudioClips { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        MSource = GetComponent<AudioSource>();
        AudioClips = Clipers;

        if (AudioClips == null || AudioClips.Length == 0)
            Debug.LogWarning("AudioManager: Ses klipleri atanmadı!");
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("AudioManager: Çalınacak ses klibi bulunamadı!");
            return;
        }

        if (MSource == null)
        {
            Debug.LogWarning("AudioManager: AudioSource bulunamadı!");
            return;
        }

        MSource.PlayOneShot(clip, 1f);
    }
}