using UnityEngine;
using System;
public class SmashManager : MonoBehaviour
{
    public static SmashManager Instance { get; private set; }

    [SerializeField] FloatingTextSpawner floatingTextSpawner;

    private GameObject bad;

    //[SerializeField] private GameObject player;

    public event Action OnSmashed;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void HandleSmashed(FallingObjectType type)
    {
        switch (type)
        {
            case FallingObjectType.Bad:
                OnSmashed?.Invoke();
                bad = GameObject.FindGameObjectWithTag("bad");
                floatingTextSpawner.Spawn(bad.transform.position);
                Debug.Log("HandleSmashed worked");
                break;
        }
    }
}
