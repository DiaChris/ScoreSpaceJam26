using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private List<AudioClip> _AudioClips;

    private AudioClip _currentClip;
    public int _currentClipIndex = 0;
    
    void Start()
    {
        StartCoroutine(NextAudioCo());
    }

    void Update()
    {
        //Debug.Log(_AudioSource.time);

        
    }

    void NextAudioClip()
    {
       
               
    }

    IEnumerator NextAudioCo()
    {
        while(true)
        {
            _AudioSource.PlayOneShot(_AudioClips[_currentClipIndex]);
            yield return new WaitForSeconds(_AudioClips[_currentClipIndex].length);
        }
    }
    
}
