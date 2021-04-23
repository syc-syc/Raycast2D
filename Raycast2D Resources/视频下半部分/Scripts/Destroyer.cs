using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float timer;

    private void Start()
    {
        Destroy(gameObject, timer);
    }
}
