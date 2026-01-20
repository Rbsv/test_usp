using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public MainUI menuUI; // seperate classs to make it look better on editior and clutter free class is at very bottom of this script

    public void Start()
    {
        GameEvents.response += OnReceiveData;
        GameEvents.OnErrorApi += OnError;
       menuUI.SearchButton.onClick.AddListener(() =>
       {
           GameEvents.onSearchClick?.Invoke(menuUI.SearchInputField.text);
       });
    }
 /// <summary>
 /// when received data as wrapper i am showing here the text and also calling the audio url file
 /// </summary>
 /// <param name="model"></param>
    void OnReceiveData(DictWrapper model)
    {
        var item = model.items[0];
      
       string audiourl = "";
       if (item.phonetics.Count == 0)
       {
           menuUI.output_Txt.text="no phonetics found";
           return;
       }
       if (item.phonetics.Count > 1)
       {
           foreach (var phonetic in item.phonetics)
           {
               if (!string.IsNullOrEmpty(phonetic.audio))
               {
                   audiourl =  phonetic.audio;
                   break;
               }
           }
          // audiourl = string.IsNullOrEmpty(item.phonetics[0].audio)?  item.phonetics[1].audio :  item.phonetics[0].audio;
       }
       else
       {
           audiourl = item.phonetics[0].audio;
       }

       Debug.Log("Audio URL "+audiourl);
        GameEvents.OnSoundURLPlay?.Invoke(audiourl); // invoking sound url on receive audio url
       
        
        menuUI.output_Txt.text =(string.IsNullOrEmpty(item.phonetics[0].text) ? "" : $"Phonetic: <b>{item.phonetics[0].text}</b>\n") +
                                $"Meaning: {item.meanings[0].definitions[0].definition}\n" +
                                (string.IsNullOrEmpty(item.meanings[0].definitions[0].example) ? "" : $"Example: {item.meanings[0].definitions[0].example}\n");
    }

    void OnError(ErrorMessage error)
    {
        menuUI.output_Txt.text = $"{error.title}\n{error.message}";
    }

    private void OnDestroy()
    {
        GameEvents.response -= OnReceiveData;
        GameEvents.OnErrorApi -= OnError;
    }
}
[System.Serializable]
public class MainUI
{
    public Button SearchButton;
    public TMPro.TMP_InputField SearchInputField;
    public TMPro.TextMeshProUGUI output_Txt;
}
