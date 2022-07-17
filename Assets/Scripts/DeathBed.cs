using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBed : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.OnPlayerDead();
            Destroy(other.gameObject); 
        }
    }
}
