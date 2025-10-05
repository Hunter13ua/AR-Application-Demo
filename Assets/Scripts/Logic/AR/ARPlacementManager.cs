using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManager : MonoBehaviour
{
    public event Action OnModelPlaced;

    private float timer = 0;

    private void Update()
    {
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;

            // Check if gesture is being performed
            if (touch.isInProgress)
            {
                timer += Time.deltaTime;
            }

            // if we released quickly (within 0.5s) we consider this a tap
            // otherwise it's likely a gesture
            if (touch.press.wasReleasedThisFrame && timer < 0.5f)
            {
                Vector2 touchPosition = touch.position.ReadValue();
                TryPlaceModel(touchPosition);
            }
            
            if (touch.press.wasReleasedThisFrame)
            {
                timer = 0;
            }
        }
    }

    private void TryPlaceModel(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        bool hitModel = Physics.Raycast(ray, out RaycastHit hit);
        ModelInteraction hitInteraction = hitModel ? hit.collider.GetComponent<ModelInteraction>() : null;

        if (ModelInteraction.SelectedObject != null)
        {
            // Something is selected
            if (hitInteraction != null)
            {
                // Tapped on an object - toggle selection
                hitInteraction.Select();
            }
            else
            {
                // Tapped elsewhere - deselect
                ModelInteraction.SelectedObject.Deselect();
            }
        }
        else
        {
            // Nothing selected
            if (hitInteraction != null)
            {
                // Tapped on an object - select it
                hitInteraction.Select();
            }
            else
            {
                // Tapped on plane - place new object
                List<ARRaycastHit> arHits = new List<ARRaycastHit>();
                if (ServiceLocator.Instance.ARRaycastManager.Raycast(screenPosition, arHits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = arHits[0].pose;
                    GameObject modelPrefab = ServiceLocator.Instance.ModelLoader.LoadedModel;
                    if (modelPrefab != null)
                    {
                        GameObject placedModel = Instantiate(modelPrefab, hitPose.position, hitPose.rotation);
                        float targetSize = 0.2f;
                        float scale = targetSize / ServiceLocator.Instance.ModelLoader.ModelSize;
                        placedModel.transform.localScale = Vector3.one * scale;
                        placedModel.SetActive(true);

                        OnModelPlaced?.Invoke();
                    }
                }
            }
        }
    }
}
