using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechPlayer : MonoBehaviour
{
    public Speech speech;

    [SerializeField]
    private AudioSource[] sourses;

    private int activeSourceIndex;
    private Coroutine volumeRoutine, textRoutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlaySpeech(speech);
        }
    }


    public void PlaySpeech(Speech speech, float fadeDuration = 0.5f)
    {
        activeSourceIndex = 1 - activeSourceIndex;
        sourses[activeSourceIndex].clip = speech.clip;
        sourses[activeSourceIndex].Play();

        if (volumeRoutine != null) StopCoroutine(volumeRoutine);
        volumeRoutine = StartCoroutine(AnimateSoundCrossfade(fadeDuration));
        if (textRoutine != null) StopCoroutine(textRoutine);
        textRoutine = StartCoroutine(SpeachRoutine(speech));
    }


    IEnumerator SpeachRoutine(Speech speech)
    {
        Subtitles subtitles = FindObjectOfType<Subtitles>();
        if (subtitles != null)
        {
            float startTime = Time.time;
            subtitles.SetActive(true);
            foreach (var line in speech.lines)
            {
                subtitles.SetSubtitlesText(line.text);
                while (Time.time < startTime + line.timeCode)
                {
                    yield return null;
                }
            }
            subtitles.SetActive(false);
        }
        textRoutine = null;
    }

    IEnumerator AnimateSoundCrossfade(float duration)
    {
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.unscaledDeltaTime * 1 / duration;
            sourses[activeSourceIndex].volume = Mathf.Lerp(0, 1, percent);
            sourses[1 - activeSourceIndex].volume = Mathf.Lerp(1, 0, percent);
            yield return null;
        }
        volumeRoutine = null;
    }
}
