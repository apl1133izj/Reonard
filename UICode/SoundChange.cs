using UnityEngine;

public class SoundChange : MonoBehaviour
{
    public BridgeBuild bridgeBuild;
    public AudioSource audioSource;
    public AudioClip audioClip;
    int audioClipchangeCount;

    private void Awake()
    {
        

    }
    void Update()
    {
        if ((int)bridgeBuild.bridgeBuildTime == 0 && audioClipchangeCount == 0)
        {
            audioClipchangeCount += 1;
            audioSource.clip = audioClip;
            audioSource.volume = 1;
            audioSource.Play();
            Debug.Log("À½¾Ç ¹Ù²Ù±â");
        }
    }
}
