using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public enum SFXType { Pickup}

    public static AudioManager AM;

    private AudioSource _source;

    private void Awake()
    {
        transform.SetParent(null);

        #region Singleton
        if (AM == null)
        {
            AM = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
        #endregion

        _source = GetComponent<AudioSource>();
    }
    public static void PlayOneShot(SFXType sfxType)
    {
        //AudioSource source = 
    }
}
