using UnityEngine;

public class DiceSide : MonoBehaviour
{
    public Vector3 Direction => transform.up;
    [SerializeField] private string number;
    public string Number => number;
}