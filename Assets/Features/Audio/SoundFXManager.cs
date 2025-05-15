using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SoundFXManager : MonoBehaviour
{
   public static SoundFXManager instance; 
    public AudioSource soundFXObject; 

   private void Awake()
   {
     if (instance == null)
     {
        instance = this; 
     }
   }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn in gameobject
        // AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.Identity);
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, spawnTransform.rotation);

        //assign the audioclip
        audioSource.clip = audioClip;

        //assign volume
        audioSource.volume = volume;

        //play sound
        audioSource.Play();

        //get length of sound FX clip
        float clipLength = audioSource.clip.length;

        //destroy the clip after it is done playing
        Destroy(audioSource.gameObject, clipLength);
    }

}
