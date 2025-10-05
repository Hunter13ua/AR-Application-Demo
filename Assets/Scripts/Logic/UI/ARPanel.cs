public class ARPanel : UIPanel
{
    public override UIStateManager.UIState PanelState => UIStateManager.UIState.ARScreen;

    public override void Show()
    {
        ServiceLocator.Instance.EnableAR();
        gameObject.SetActive(true);
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void Initialize()
    {

    }
}
