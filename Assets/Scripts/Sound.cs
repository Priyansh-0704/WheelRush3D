using UnityEngine;

[System.Serializable]

public class Sound
{
    public AudioClip clip;
    public AudioSource audioSource;

    public float volume;
    public string name;
    public bool loop;
}
