using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOfDeath : MonoBehaviour
{

    [SerializeField] public float time;
    public float levelLength;
    public float velocity;

    public GameObject StartPos, EndPos, Player, HandModel;

    public float levelProgression;
    private bool levelStarted;

    void Start()
    {
        EventBroker.Instance.OnStartLevel += StartMoving;

        StartPos = GameManager.Instance.StartPos;
        EndPos = GameManager.Instance.EndPos;
        Player = GameObject.FindWithTag("Player"); 

        transform.position = StartPos.transform.position;

        levelLength = EndPos.transform.position.z - StartPos.transform.position.z;
        if (time <= 0) time = 10;
        velocity = levelLength / time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelStarted) return;
        
        if (transform.position.z >= EndPos.transform.position.z) return;
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);

        levelProgression = transform.position.z / levelLength;
        GameManager.Instance.HandProgression = levelProgression;

        if (Player == null) return;

        var xPos = Player.transform.position.x;
        var xVec = new Vector3(xPos, transform.position.y, transform.position.z);
        var horizontalSpeed = 1;
        transform.position = Vector3.MoveTowards(transform.position, xVec, horizontalSpeed * Time.deltaTime);

        if (GameManager.Instance.PlayerDead)
        {
            ScaleUpHand(); 
        }
    }

    private void StartMoving() => levelStarted = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventBroker.Instance.OnFailLevel?.Invoke(); 
            velocity = 0; 
        }
    }

    private void ScaleUpHand()
    {
        HandModel.transform.localScale += new Vector3(1, 1, 0)*500 * Time.deltaTime; 
    }

    public void IncreaseVelocity()
    {
        velocity += 0.1f;
    }

    public void DecreaseVelocity()
    {
        velocity -= 0.1f;
    }
    public void ChangeVelocity(float vel)
    {
        velocity = vel;
    }

    private void OnDestroy()
    {
        EventBroker.Instance.OnStartLevel -= StartMoving;
    }
}
