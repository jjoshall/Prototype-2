using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public static GravityManager Instance { get; private set; }

    public Vector3[] gravityDirections = new Vector3[]
    {
        Vector3.up,
        Vector3.down,
        Vector3.left,
        Vector3.right,
        Vector3.forward,
        Vector3.back
    };

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetGravityDirection(Vector3 newDirection)
    {
        Physics.gravity = newDirection * 9.81f;
    }
}
