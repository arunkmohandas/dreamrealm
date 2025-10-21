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
        // Create a ray from the camera through the mouse/touch position
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        // Raycast hit info
        RaycastHit hit;

        // Perform raycast against objects in interactableLayer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            Card card = hit.collider.GetComponent<Card>();
            if (card != null)
            {
                Debug.Log("Touched object: " + card.CardID);

                if (card.IsOpen)
                    card.FlipClose();
                else
                    card.FlipOpen();
            }
        }
    }

}
