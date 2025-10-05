using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance => instance;
    private static ServiceLocator instance;

    [field: SerializeField] public UIStateManager UIStateManager { get; private set; }
    [field: SerializeField] public ModelLoader ModelLoader { get; private set; }
    [field: SerializeField] public ARSession ARSession { get; private set; }
    [field: SerializeField] public ARPlaneManager ARPlaneManager { get; private set; }
    [field: SerializeField] public ARRaycastManager ARRaycastManager { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnableAR()
    {
        if (ARSession != null) ARSession.gameObject.SetActive(true);
        if (ARPlaneManager != null) ARPlaneManager.enabled = true;
        if (ARRaycastManager != null) ARRaycastManager.enabled = true;
    }
}
