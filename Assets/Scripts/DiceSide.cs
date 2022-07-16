using UnityEngine;

public class DiceSide : MonoBehaviour
{
    public Vector3 Direction
    {
        get
        {
            var dir = transform.up;
            dir.x = Mathf.Round(dir.x);
            dir.y = Mathf.Round(dir.y);
            dir.z = Mathf.Round(dir.z);
            return dir;
        }   
    }
    [SerializeField] private string number;
    public string Number => number;

    private Transform dice;
   
}