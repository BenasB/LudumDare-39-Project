using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    public static MusicPlayer Instance;

    AudioSource source;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeVolume(float volume)
    {
        source.volume = volume;
    }
}
