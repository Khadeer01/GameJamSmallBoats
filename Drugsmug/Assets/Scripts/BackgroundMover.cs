using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField] float backgroundMoveSpeed = 1.0f;

    [SerializeField] SpriteRenderer background01;
    [SerializeField] SpriteRenderer background02;

    SpriteRenderer currentMainMovingBackground;

    Vector2 startPosition = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMainMovingBackground = background01;
        startPosition = currentMainMovingBackground.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 movePosition = new Vector2(0.0f, )
        currentMainMovingBackground.transform.position += Vector3.down * backgroundMoveSpeed * Time.deltaTime;
        FollowOtherBackground();
    }

    void FollowOtherBackground()
    {
        if (currentMainMovingBackground == background01)
        {
            Vector2 topLeft = new Vector3(
                background02.bounds.min.x, // left
                background02.bounds.max.y // top
            );


            Vector2 topLeftScreenVector = new Vector2(0.0f, Screen.height);
            float outOfScreenMag = (topLeftScreenVector - topLeft).magnitude ;
            print("Screen distance: " + outOfScreenMag);
            //Camera camera = Camera.main;
            //if (camera != null)

            //{
            //    if (camera.WorldToScreenPoint()
            //}        
        }
        else if (currentMainMovingBackground != background02)
        {

        }
    }

    
}
