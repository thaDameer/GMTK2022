using System;
using UnityEngine;

public class EventBroker {
    static EventBroker _instance;
    EventBroker() { }
    
    public static EventBroker Instance => _instance ??= new EventBroker();

    public Func<DiceSide> OnDiceSideChanged;
}