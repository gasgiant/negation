using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechStarter : MonoBehaviour
{
    public Speech speech;
    public AudioClip music;
    public bool startOnStart;

    private void Start()
    {
        if (startOnStart)
        {
            SpeechPlayer speechPlayer = FindObjectOfType<SpeechPlayer>();
            if (speechPlayer != null && speech != null)
                speechPlayer.PlaySpeech(speech);
            if (AudioManager.Instance != null && music != null)
                AudioManager.Instance.PlayMusic(music);
            Debug.Log("StartAudio");
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !startOnStart)
        {
            SpeechPlayer speechPlayer = FindObjectOfType<SpeechPlayer>();
            if (speechPlayer != null && speech != null)
                speechPlayer.PlaySpeech(speech);
            if (AudioManager.Instance != null && music != null)
                AudioManager.Instance.PlayMusic(music);
            Debug.Log("StartAudio");
            gameObject.SetActive(false);
        }
    }
}
