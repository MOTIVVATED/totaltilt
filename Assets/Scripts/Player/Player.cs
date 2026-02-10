using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out FallingObject fallingObject))
        {
            fallingObject.Collect();
        }
    }
}
