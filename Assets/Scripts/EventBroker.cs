using System;
using UnityEngine;

public class EventBroker {
    static EventBroker _instance;
    EventBroker() { }
    
    public static EventBroker Instance => _instance ??= new EventBroker();

    public Action<float> OnLevelCountdownStart;
    public Action OnStartLevel;
    public Action OnCompleteLevel;
    public Action OnFailLevel;
    public Action OnGameReset;
}