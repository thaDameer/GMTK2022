using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class UIEyeController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartUp());
        Debug.Log("Starting");
    }

    private IEnumerator StartUp()
    {
        Random rnd = new Random();
        string str;
        str = rnd.Next(2) == 1 ? "Blink" : "MoveEye";

        Debug.Log("str");
        animator.SetTrigger(str);
        yield return new WaitForSeconds(rnd.Next(5));

        StartCoroutine(StartUp());
    }
}