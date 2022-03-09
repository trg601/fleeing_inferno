using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum Clip {
        HardHit,
        BrickBreak,
        SoftHit,
        PlayerDeath
    }

    public AudioClip[] clips;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClip(Clip clip, float volume = 1f) {
        audioSource.PlayOneShot(clips[(int) clip], volume);
    }
}
