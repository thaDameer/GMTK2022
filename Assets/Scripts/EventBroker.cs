using System;
using UnityEngine;

public class EventBroker {
    static EventBroker _instance;
    EventBroker() { }
    
    public static EventBroker Instance => _instance ??= new EventBroker();

    public Func<DiceSide> OnDiceSideChanged;
    public Action<float> OnLevelCountdownStart;
    public Action OnGameplaySceneLoaded;
    public Action OnStartLevel;
    public Action OnCompleteLevel;
    public Action OnFailLevel;
}