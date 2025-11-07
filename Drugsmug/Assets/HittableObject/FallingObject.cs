using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] float fallspeed = (1f);

    private void Start()
    {
    }

    private void Update()
    {
        transform.Translate(Vector2.down * fallspeed * Time.deltaTime);
    }
}
