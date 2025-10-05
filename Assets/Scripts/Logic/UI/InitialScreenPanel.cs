using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitialScreenPanel : UIPanel
{
    private enum SubState { Input, Loading, Success }

    public override UIStateManager.UIState PanelState => UIStateManager.UIState.InitialScreen;

    [field: SerializeField] public TMP_InputField InputField { get; private set; }
    [field: SerializeField] public Button SubmitButton { get; private set; }
    [field: SerializeField] public GameObject LoadingNotif { get; private set; }
    [field: SerializeField] public Button SuccessButton { get; private set; }
    private GlbUrlValidator urlValidator = new();
    private SubState currentSubState;

    public override void Show()
    {
        gameObject.SetActive(true);
        currentSubState = SubState.Input;
        UpdateUI();
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

        if (SuccessButton != null)
        {
            SuccessButton.onClick.AddListener(ProceedToAR);
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

        currentSubState = SubState.Loading;
        UpdateUI();

        ServiceLocator.Instance.ModelLoader.LoadModel(
            url,
            OnModelLoaded,
            OnModelLoadFailed
        );
    }

    private void OnModelLoaded()
    {
        currentSubState = SubState.Success;
        UpdateUI();
    }

    private void OnModelLoadFailed(string error)
    {
        Debug.Log($"Failed to load model: {error}");
        currentSubState = SubState.Input;
        UpdateUI();
    }

    private void ProceedToAR()
    {
        ServiceLocator.Instance.EnableAR();
        ServiceLocator.Instance.UIStateManager.SetState(UIStateManager.UIState.ARScreen);
    }

    private void UpdateUI()
    {
        if (InputField != null) InputField.gameObject.SetActive(currentSubState == SubState.Input);
        if (SubmitButton != null) SubmitButton.gameObject.SetActive(currentSubState == SubState.Input);
        if (LoadingNotif != null) LoadingNotif.gameObject.SetActive(currentSubState == SubState.Loading);
        if (SuccessButton != null) SuccessButton.gameObject.SetActive(currentSubState == SubState.Success);
    }
}
