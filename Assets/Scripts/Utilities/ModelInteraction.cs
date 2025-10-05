using UnityEngine;
using UnityEngine.InputSystem;

public class ModelInteraction : MonoBehaviour
{
    public static ModelInteraction SelectedObject { get; private set; }

    private Renderer[] renderers;

    private Vector2 initialTouchPosition;
    private float initialDistance;
    private Vector3 initialScale;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void Select()
    {
        if (SelectedObject == this)
        {
            Deselect();
        }
        else
        {
            if (SelectedObject != null)
            {
                SelectedObject.Deselect();
            }
            SelectedObject = this;
            UpdateVisual();
        }
    }

    public void Deselect()
    {
        SelectedObject = null;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (var renderer in renderers)
        {
            renderer.material.color = SelectedObject == this ? Color.yellow : Color.white;
        }
    }

    private void Update()
    {
        if (SelectedObject != this) return;

        if (Touchscreen.current != null)
        {
            var touches = Touchscreen.current.touches;

            // Count active touches
            int activeTouches = 0;
            for (int i = 0; i < touches.Count; i++)
            {
                if (touches[i].isInProgress) activeTouches++;
            }

            if (activeTouches == 2)
            {
                // Pinch scaling
                var touch1 = touches[0];
                var touch2 = touches[1];

                if (touch1.press.wasPressedThisFrame || touch2.press.wasPressedThisFrame)
                {
                    initialDistance = Vector2.Distance(touch1.position.ReadValue(), touch2.position.ReadValue());
                    initialScale = transform.localScale;
                }
                else
                {
                    float currentDistance = Vector2.Distance(touch1.position.ReadValue(), touch2.position.ReadValue());
                    float scaleFactor = currentDistance / initialDistance;
                    transform.localScale = initialScale * scaleFactor;
                }
            }
            else if (activeTouches == 1)
            {
                // Single finger rotation
                var touch = touches[0];
                float deltaX = -touch.delta.ReadValue().x;
                transform.Rotate(Vector3.up, deltaX * 0.5f);
            }
        }
    }
}
