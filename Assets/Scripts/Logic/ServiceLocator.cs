using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance => instance;
    private static ServiceLocator instance;

    [field: SerializeField] public UIStateManager UIStateManager { get; private set; }
    [field: SerializeField] public ModelLoader ModelLoader { get; private set; }

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
}
