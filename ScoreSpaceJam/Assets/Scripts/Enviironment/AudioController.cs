using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private List<AudioClip> _AudioClips;

    [SerializeField] private AudioClip _DeathMusic;

    public int _currentClipIndex = 0;
    
    void Start()
    {
        StartCoroutine(NextAudioCo());
    }

    public void SetTrack(int index)
    {
        _currentClipIndex = index;
    }

    IEnumerator NextAudioCo()
    {
        while(true)
        {
            _AudioSource.PlayOneShot(_AudioClips[_currentClipIndex]);
            yield return new WaitForSeconds(_AudioClips[_currentClipIndex].length);
        }
    }

    public void PlayDeathMusic()
    {
        StopAllCoroutines();
        _AudioSource.Stop();

        _AudioSource.PlayOneShot(_DeathMusic, 0.15f);
    }
    
}
