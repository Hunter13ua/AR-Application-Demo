using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitialScreenPanel : UIPanel
{
    public override UIStateManager.UIState PanelState => UIStateManager.UIState.InitialScreen;

    [field: SerializeField] public TMP_InputField InputField { get; private set; }
    [field: SerializeField] public Button SubmitButton { get; private set; }
    private GlbUrlValidator urlValidator = new();

    public override void Show()
    {
        gameObject.SetActive(true);
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
    }

    private void ValidateInput(string text)
    {
        if (SubmitButton != null)
        {
            SubmitButton.interactable = urlValidator.IsValid(text);
        }
    }
}
