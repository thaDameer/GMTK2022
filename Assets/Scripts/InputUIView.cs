using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputUIView : MonoBehaviour
{
    [SerializeField] private Image leftTab, rightTab;
    [SerializeField] private TMP_Text leftText, rightText, jumpText;
    [SerializeField] private Transform camera;
    [SerializeField] private Transform parent;

    void Awake()
    {
        CubeController.OnDiceSideChanged += UpdatedDiceView;
        parent = transform.parent;
        camera = Camera.main.transform;
    }

    private void Start()
    {
        if (!camera)
        {
            camera = Camera.main.transform;
        }
    }

    private void LateUpdate()
    {
        if (camera)
        {
            //parent.rotation.SetLookRotation(camera.position);
            parent.LookAt(camera.position);
        }
    }

    private void OnDestroy()
    {
        CubeController.OnDiceSideChanged -= UpdatedDiceView;
    }

    private void UpdatedDiceView(DiceSide left, DiceSide jump, DiceSide right)
    {
        leftText.text = left.Number.ToString();
        jumpText.text = jump.Number.ToString();
        rightText.text = right.Number.ToString();
    }
}