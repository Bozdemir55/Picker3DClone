using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public AudioSource sourceAudio;
    public AudioClip collect;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Setup()
    {
        GameManager.instance.voiceManager= FindObjectOfType<VoiceManager>();
        sourceAudio = transform.GetComponent<AudioSource>();
    }
    public void PlayCollectVoice()
    {
        sourceAudio.clip = collect;
        sourceAudio.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
