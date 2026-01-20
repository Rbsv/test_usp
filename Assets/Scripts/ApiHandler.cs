using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ApiHandler : MonoBehaviour
{
    // used for managing apis 
   public static ApiHandler Instance { get; private set; }

   private void Awake()
   {
       Instance = this;
       GameEvents.onSearchClick += SendWord;
   }

   private void OnDestroy()
   {
       GameEvents.onSearchClick -= SendWord;
   }

   void SendWord(string word)
   {
       if (string.IsNullOrEmpty(word))
       {
           Debug.LogError("Please enter a word");
           return;
       }
       StartCoroutine(IE_SendWord(word));
   }

   IEnumerator IE_SendWord(string word)
   {
       
       using UnityWebRequest request = UnityWebRequest.Get(Constants.baseURL+word);
       yield return request.SendWebRequest();
       if (!request.isDone)
       {
           Debug.LogError(request.error);
       }
       else
       {
           Debug.Log(request.downloadHandler.text);
           if (request.downloadHandler.text[0] == '{') // when begin with { then i am getting error response.
           {
               var json = JsonUtility.FromJson<ErrorMessage>(request.downloadHandler.text);
               GameEvents.OnErrorApi?.Invoke(json);
           }
           else if (request.downloadHandler.text[0] == '[') // when begin with [ then i am getting correct response.
           {
               var wrappString  = "{\"items\":" + request.downloadHandler.text + "}"; // i used a simple trick to wrap in a simpler way
               var jsonObj = JsonUtility.FromJson<DictWrapper>(wrappString);
               GameEvents.response?.Invoke(jsonObj);
           }
          
       }
   }
}
