using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBed : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            EventBroker.Instance.OnFailLevel?.Invoke();
    }
}
