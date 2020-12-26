using System.Collections.Generic;
using UnityEngine;

public class AudioCache : MonoBehaviour
{
    private static Dictionary<string, AudioClip> cache = new Dictionary<string, AudioClip>();
    private static AudioCache instance = null;
    private GameObject oneShotPlayerPrefab = null;

    private List<AudioSource> playingAudioSources = new List<AudioSource>();

    public static AudioCache GetInstance()
    {
        return AudioCache.instance;
    }

    private void Awake()
    {
        if (AudioCache.instance != null)
        {
            throw new System.Exception("SoundCache is singleton");
        }
        
        AudioCache.instance = this;

        this.oneShotPlayerPrefab = Resources.Load<GameObject>("Prefab/OneShotPlayer");
    }

    private void Update()
    {
        for (int i = this.playingAudioSources.Count - 1; i >= 0; i--)
        {
            AudioSource audio = this.playingAudioSources[i];
            if (!audio.isPlaying)
            {
                this.playingAudioSources.RemoveAt(i);
                Destroy(audio.gameObject);
            }
        }
    }

    public void AddCache(string name, AudioClip clip)
    {
        if (!AudioCache.cache.ContainsKey(name))
        {
            AudioCache.cache.Add(name, clip);
        }
    }
    public AudioClip GetCache(string name)
    {
        return AudioCache.cache[name];
    }

    public void OneShot(string cacheName)
    {
        this.OneShot(cacheName, false);
    }
    public void OneShot(string cacheName, bool allowDuplicatePlayback)
    {
        AudioClip clip = AudioCache.cache[cacheName];
        
        if (clip == null)
        {
            return;
        }

        if (!allowDuplicatePlayback)
        {
            for (int i = 0; i < this.playingAudioSources.Count; i++)
            {
                AudioSource playingAudio = this.playingAudioSources[i];
                if (playingAudio.clip != null && playingAudio.clip.name == clip.name)
                {
                    return;
                }
            }
        }

        GameObject player = Instantiate(oneShotPlayerPrefab);
        AudioSource audio = player.GetComponent<AudioSource>();
        audio.PlayOneShot(AudioCache.cache[cacheName]);
        this.playingAudioSources.Add(audio);
    }
}
