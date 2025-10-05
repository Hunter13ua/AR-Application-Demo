using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitialScreenPanel : UIPanel
{
    public override UIStateManager.UIState PanelState => UIStateManager.UIState.InitialScreen;

    [field: SerializeField] public TMP_InputField InputField { get; private set; }
    [field: SerializeField] public Button SubmitButton { get; private set; }
    [field: SerializeField] public GameObject LoadingNotif { get; private set; }
    private GlbUrlValidator urlValidator = new();

    public override void Show()
    {
        gameObject.SetActive(true);
        ShowLoadingNotif(false);
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void Initialize()
    {
        if (InputField != null)
        {
            InputField.onValueChanged.AddListener(ValidateInput);
            ValidateInput(InputField.text); // Initial validation
        }

        if (SubmitButton != null)
        {
            SubmitButton.onClick.AddListener(Submit);
        }
    }

    private void ValidateInput(string text)
    {
        if (SubmitButton != null)
        {
            SubmitButton.interactable = urlValidator.IsValid(text);
        }
    }

    private void Submit()
    {
        string url = InputField?.text;
        if (string.IsNullOrEmpty(url) || !urlValidator.IsValid(url)) return;

        ShowLoadingNotif(true);

        ServiceLocator.Instance.ModelLoader.LoadModel(
            url,
            () => ServiceLocator.Instance.UIStateManager.SetState(UIStateManager.UIState.ARScreen),
            (error) => Debug.Log($"Failed to load model: {error}")
        );
    }

    private void ShowLoadingNotif(bool value)
    {
        InputField.gameObject.SetActive(!value);
        SubmitButton.gameObject.SetActive(!value);
        LoadingNotif.gameObject.SetActive(value);
    }
}
