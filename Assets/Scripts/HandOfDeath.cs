using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOfDeath : MonoBehaviour
{

    [SerializeField] public float time;
    public float levelLength;
    public float velocity;

    public GameObject StartPos, EndPos;

    public float levelProgression;


    void Start()
    {
        StartPos = GameManager.Instance.startPos; //GET FROM GAMEMANAGER
        EndPos = GameManager.Instance.endPos;

        transform.position = StartPos.transform.position;

        levelLength = EndPos.transform.position.z - StartPos.transform.position.z;
        if (time <= 0) time = 10;
        velocity = levelLength / time;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= EndPos.transform.position.z) return;
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);

        levelProgression = transform.position.z / levelLength;
        GameManager.Instance.handProgression = levelProgression;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.playerDead = true;
            GameManager.Instance.OnPlayerDead(); 
            velocity = 0; 
        }
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

}
