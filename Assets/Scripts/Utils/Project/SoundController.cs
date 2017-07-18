using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public enum SoundName
{
    CLICK,
    BLOB,
    FALL_ON_SPIKES
}

[System.Serializable]
public struct Sound
{
    public SoundName name;
    public AudioClip audio;
}

public class SoundController : MonoBehaviour
{
    private static SoundController _instance;
    public static SoundController instance
    {
        get { return _instance; }
    }

    private bool inited = false;

    [SerializeField] AudioSource musicAS;
    [SerializeField] AudioSource sfxAS;

    [SerializeField] Sound[] sounds;
    Dictionary<SoundName, AudioClip> soundsDictionary = new Dictionary<SoundName, AudioClip>();
    [SerializeField] List<AudioClip> musicBG = new List<AudioClip>();

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    void Start()
    {
        foreach (var item in sounds)
            soundsDictionary.Add(item.name, item.audio);
    }

    public void Init()
    {
        foreach (SoundName key in soundsDictionary.Keys.ToList())
        {
            switch (key)
            {
                case SoundName.CLICK:
                    soundsDictionary[key] = Main.Instance.bundle.LoadAsset<AudioClip>("assets/sounds/click.mp3");
                    break;
                case SoundName.BLOB:
                    soundsDictionary[key] = Main.Instance.bundle.LoadAsset<AudioClip>("assets/sounds/blob.mp3");
                    break;
                case SoundName.FALL_ON_SPIKES:
                    soundsDictionary[key] = Main.Instance.bundle.LoadAsset<AudioClip>("assets/sounds/fall_on_spike.mp3");
                    break;
            }
        }
        inited = true;
    }


    public void PlaySound(SoundName name, float volume = 1, float pitch = 1)
    {
        if (!soundsDictionary.ContainsKey(name) || !inited)
            return;
        sfxAS.pitch = pitch;
        sfxAS.PlayOneShot(soundsDictionary[name], volume);
    }

}
