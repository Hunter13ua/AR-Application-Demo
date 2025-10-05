using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPanel : UIPanel
{
    private enum ARSubState { Scanning, ReadyToPlace, Placed }

    public override UIStateManager.UIState PanelState => UIStateManager.UIState.ARScreen;

    [field: SerializeField] public GameObject ScanLabel { get; private set; }
    [field: SerializeField] public GameObject PlaceLabel { get; private set; }

    private ARSubState currentSubState;

    public override void Show()
    {
        gameObject.SetActive(true);
        currentSubState = ARSubState.Scanning;
        UpdateUI();
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void Initialize()
    {
        ServiceLocator.Instance.ARPlaneManager.trackablesChanged.AddListener(OnPlanesChanged);
    }

    private void OnPlanesChanged(ARTrackablesChangedEventArgs<ARPlane> args)
    {
        if (currentSubState == ARSubState.Scanning && ServiceLocator.Instance.ARPlaneManager.trackables.count > 0)
        {
            currentSubState = ARSubState.ReadyToPlace;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (ScanLabel != null) ScanLabel.SetActive(currentSubState == ARSubState.Scanning);
        if (PlaceLabel != null) PlaceLabel.SetActive(currentSubState == ARSubState.ReadyToPlace);
        // Placed state will hide both
    }
}
