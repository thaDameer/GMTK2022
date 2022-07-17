using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Random = System.Random;

public class CubeEyeController : MonoBehaviour
{
    [SerializeField]
    private List<Material> usedMaterials = new List<Material>(); // Start is called before the first frame update

    [SerializeField] private float blinkSpeed = 0.2f;
    [SerializeField] private float lookSpeed = 0.2f;
    [SerializeField] private int maxWaitDuration = 5;

    [SerializeField] private List<Texture> eyeBlinkingTextures;
    [SerializeField] private List<Texture> eyeLooking;

    private Coroutine animation;

    void Start()
    {
        var list = GetComponentsInChildren<MeshRenderer>();
        foreach (var meshRenderer in list)
        {
            usedMaterials.Add(meshRenderer.material);
        }

        Blink();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Blink();
        }
    }


    private IEnumerator StartupRoutine()
    {
        Random rnd = new Random();
        yield return new WaitForSeconds(rnd.Next(0, 1));
        Blink();
    }

    private void Blink()
    {
        if (animation != null)
            StopCoroutine(animation);
        animation = StartCoroutine(EyeAnimation());
    }

    private IEnumerator EyeAnimation()
    {
        Random rnd = new Random();
        for (int i = 0; i < eyeBlinkingTextures.Count; i++)
        {
            foreach (var material in usedMaterials)
            {
                material.mainTexture = eyeBlinkingTextures[i];
            }

            yield return new WaitForSecondsRealtime(blinkSpeed);
        }

        yield return new WaitForSeconds(rnd.Next(0, maxWaitDuration));
        if (rnd.Next(0, 1) == 1)
        {
            animation = StartCoroutine(MoveEye());
        }
        else
        {
            animation = StartCoroutine(EyeAnimation());
        }
    }

    private IEnumerator MoveEye()
    {
        Random rnd = new Random();
        for (int i = 0; i < eyeLooking.Count; i++)
        {
            foreach (var material in usedMaterials)
            {
                material.mainTexture = eyeLooking[i];
            }

            yield return new WaitForSecondsRealtime(lookSpeed);
        }

        yield return new WaitForSeconds(rnd.Next(0, maxWaitDuration));
        if (rnd.Next(0, 2) == 1)
        {
            animation = StartCoroutine(MoveEye());
        }
        else
        {
            animation = StartCoroutine(EyeAnimation());
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(animation);
    }
}