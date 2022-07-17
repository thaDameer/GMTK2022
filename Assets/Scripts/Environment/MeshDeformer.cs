using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{
    public float displacementAmount = 0.5f;
    public float force = 1f;
    public ParticleSystem SplashParticleSystem;
    private MeshRenderer meshRenderer;

    private bool isTriggered = false;
    [SerializeField] private Transform playerPosition;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        CubeController.OnDiceSideChanged += Triggered;
    }

    public void Update()
    {
        displacementAmount = Mathf.Lerp(displacementAmount, 0, Time.deltaTime);
        meshRenderer.material.SetFloat("_Amount", displacementAmount);
    }

    public void OnTriggerStay(Collider other)
    {
        if (isTriggered && other.gameObject.CompareTag("Player"))
        {
            Collide(other.gameObject.transform);
            isTriggered = !isTriggered;
        }
    }

    private void Triggered(DiceSide side1, DiceSide jump, DiceSide side2)
    {
        isTriggered = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collide(other.gameObject.transform);
        }
    }


    public void Collide(Transform playerTransform)
    {
        playerPosition = playerTransform;
        meshRenderer.material.SetVector("_DicePosition", (Vector4)playerPosition.position);
        meshRenderer.material.SetFloat("_RippleStartTime", Time.time);
        displacementAmount = force;
        if (SplashParticleSystem)
            SplashParticleSystem.Play();
    }

    private void OnDestroy()
    {
        CubeController.OnDiceSideChanged -= Triggered;
    }
}