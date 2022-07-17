using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float Timer;
    public int time = 2;

    public bool trigger;

    private void Awake()
    {
        trigger = false;
        Timer = 0;
    }
    private void Update()
    {
        if (!trigger) return;
        Timer += Time.deltaTime;
        Shake();

        //Destroy(this.gameObject, time); // VAD BLIR BÄST? Destroya efter animation! 
        if (Timer >= time)
        {
            gameObject.SetActive(false);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") trigger = true;
           
    }


    public void Shake()
    {
        transform.Translate(Vector3.right * 0.05f * Mathf.Sin(8 * Mathf.PI * Timer));
    }

}
