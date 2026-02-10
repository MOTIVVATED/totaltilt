using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal { get; private set; }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
    }

}
