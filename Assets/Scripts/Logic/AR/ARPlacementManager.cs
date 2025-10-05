using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManager : MonoBehaviour
{
    public event Action OnModelPlaced;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                TryPlaceModel(touch.position);
            }
        }
    }

    private void TryPlaceModel(Vector2 screenPosition)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (ServiceLocator.Instance.ARRaycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            GameObject modelPrefab = ServiceLocator.Instance.ModelLoader.LoadedModel;
            if (modelPrefab != null)
            {
                GameObject placedModel = Instantiate(modelPrefab, hitPose.position, hitPose.rotation);
                placedModel.transform.localScale = Vector3.one * 0.1f; // Adjust scale as needed
                placedModel.SetActive(true);

                OnModelPlaced?.Invoke();
            }
        }
    }
}
