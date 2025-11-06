using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private float fallspeed = (1f);

    private void Update()
    {
        transform.Translate(Vector2.down * fallspeed * Time.deltaTime);
    }
}
