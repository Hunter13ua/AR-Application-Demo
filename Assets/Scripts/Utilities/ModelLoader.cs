using System;
using System.Threading.Tasks;
using UnityEngine;
using GLTFast;
using System.Threading;

public class ModelLoader : MonoBehaviour
{
    public GameObject LoadedModel { get; private set; }
    private GltfImport _gltf;

    public async void LoadModel(string url, Action onSuccess, Action<string> onError)
    {
        try
        {
            _gltf = new GltfImport();
            bool success = await _gltf.Load(url);

            if (success)
            {
                await InstantiateTemplateGameObject();
                onSuccess?.Invoke();
            }
            else
            {
                onError?.Invoke("GLB load failed");
            }
        }
        catch (Exception e)
        {
            onError?.Invoke(e.Message);
        }
    }

    public async Task InstantiateTemplateGameObject()
    {
        if (_gltf != null && LoadedModel == null)
        {
            GameObject modelRoot = new GameObject("LoadedModel");
            modelRoot.transform.SetParent(transform, false);
            bool instantiated = await _gltf.InstantiateMainSceneAsync(modelRoot.transform);
            if (instantiated)
            {
                modelRoot.SetActive(false); // Hide the template
                LoadedModel = modelRoot;
            }
            else
            {
                Destroy(modelRoot);
                Debug.LogError("Instantiation failed");
            }
        }
    }
}
