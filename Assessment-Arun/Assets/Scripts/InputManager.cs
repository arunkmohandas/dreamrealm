using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Camera mainCamera;
    public LayerMask interactableLayer;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // Handle mouse input
        if (Input.GetMouseButtonDown(0))
        {
            DetectHit(Input.mousePosition);
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        // Handle touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            DetectHit(Input.GetTouch(0).position);
        }
#endif
    }

    /// <summary>
    /// Detects hit on cards
    /// </summary>
    /// <param name="screenPosition"></param>
    void DetectHit(Vector3 screenPosition)
    {
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(screenPosition);
        Vector2 touchPos = new Vector2(worldPoint.x, worldPoint.y);

        RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero, interactableLayer);

        if (hit.collider != null)
        {
            Debug.Log("Touched object: " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponent<Card>() != null)
            {
                Card card = hit.collider.gameObject.GetComponent<Card>();
                if ((card.IsOpen))
                {
                    card.FlipClose();
                }
                else
                {
                    card.FlipOpen();
                }
            }
        }
    }
}
