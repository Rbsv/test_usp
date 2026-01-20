using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AudioStreamer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    //used to control and manage audio source based on events
    AudioSource audioSource => GetComponent<AudioSource>();

    private void Start()
    {
        GameEvents.OnSoundURLPlay += playAudio;
    }

    private void OnDestroy()
    {
        GameEvents.OnSoundURLPlay -= playAudio;
    }

    void playAudio(string url) => StartCoroutine(StreamAudio(url));


    IEnumerator StreamAudio(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            Debug.LogWarning("Audio URL is empty.");
            yield break;
        }

        using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Audio Error "+request.error);
            }else if (request.result == UnityWebRequest.Result.Success)
            {
                var clip = DownloadHandlerAudioClip.GetContent(request);

                if (clip != null)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                    GameEvents.OnSoundPlay?.Invoke(true);
                    CancelInvoke(nameof(stopEvent));
                    Invoke(nameof(stopEvent), clip.length-.2f);// keeping some buffer
                }
                else
                {
                    Debug.LogError("Failed to Play from AudioClip URL.");
                }
            }
        }
    }

    void stopEvent()
    {
        GameEvents.OnSoundPlay?.Invoke(false);
    }
    
}
