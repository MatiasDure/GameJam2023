using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioManage : MonoBehaviour
{
    public static AudioManage Instance;

    [SerializeField]
    AudioClips[] clips;

    private AudioSource audioSource;

    public enum sound { ambient, wind, footsteps, crackingWood, sprouts, jumps, strafe };


    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }


    public void Play(sound soundToPlay)
    {
        foreach (var s in clips)
        {
            if(soundToPlay == s.sound)
            {
                audioSource.pitch = soundToPlay == sound.sprouts ? 2f : 1f;
                if (s.clip.Length < 2)
                {
                    audioSource.PlayOneShot(s.clip[0]);
                    return;
                }
                int r = Random.Range(0, s.clip.Length);
                audioSource.PlayOneShot(s.clip[r]);
                return;
            }
        }
    }
}


[System.Serializable]

public struct AudioClips
{
    public 
    AudioManage.sound sound;

    public 
    AudioClip[] clip; 

   



} 