using UnityEngine;
using UnityEngine.Events;

public class GameEvents
{
    // list of events which subscribed and used loosly.
    public static  UnityAction<bool> OnSoundPlay;
    public static  UnityAction<string> onSearchClick;
    public static  UnityAction<DictWrapper> response;
    public static UnityAction<string> OnSoundURLPlay;
    public static UnityAction<ErrorMessage> OnErrorApi;

}
