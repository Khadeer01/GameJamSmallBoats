
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class BackgroundMoving : MonoBehaviour
{
    [SerializeField] float backgroundMoveSpeed = 1.0f;

    [SerializeField] SpriteRenderer[] backgrounds;
    Vector2[] backgroundDefaultLocations;

    Vector2 startPosition = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StoreBackgroundDefaultLocations();
    }

    void StoreBackgroundDefaultLocations()
    {
        if (backgrounds == null || backgrounds.Length == 0) { return; }
        backgroundDefaultLocations = new Vector2[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgroundDefaultLocations[i] = backgrounds[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckEachBackgroundIsOutsideCamera();
	}

    void CheckEachBackgroundIsOutsideCamera()
    {
        if (backgrounds.Length > 0 && Camera.main != null)
        {
            for (int i = 0; i < backgrounds.Length; i++) 
            {
                SpriteRenderer background = backgrounds[i];

				background.transform.position += Vector3.down * backgroundMoveSpeed * Time.deltaTime;

                Vector3 topPositionOfBackground = background.transform.position + new Vector3(0.0f, background.bounds.extents.y, 0.0f);

				Vector3 backgroundViewportPos = Camera.main.WorldToViewportPoint(topPositionOfBackground);
				bool isOffScreen = backgroundViewportPos.x < 0 || backgroundViewportPos.x > 1 || backgroundViewportPos.y < 0;
                if (isOffScreen)
                {
                    SpriteRenderer nextBackground = GetNextAvailableBackground(background);
                    if (nextBackground != null)
                    {
						Vector3 topPositionOfNextBackground = nextBackground.transform.position + new Vector3(0.0f, nextBackground.bounds.extents.y*2, 0.0f);

						background.transform.position = topPositionOfNextBackground;
					}
				}
			}
        }
    }

    SpriteRenderer GetNextAvailableBackground(SpriteRenderer oldBackground)
    {
        if (backgrounds.Length == 0) return null;

        int nextIndex = 0;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            SpriteRenderer currentBackground = backgrounds[i];

            if (oldBackground == currentBackground)
            {
                nextIndex = i;
                if (i >= backgrounds.Length - 1)
                {
					nextIndex = 0;
                }
                else
                {
                    nextIndex = nextIndex + 1;
                }
            }

        }

        return backgrounds[nextIndex];
    }
  }