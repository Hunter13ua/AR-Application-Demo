public class ARPanel : UIPanel
{
    public override UIStateManager.UIState PanelState => UIStateManager.UIState.ARScreen;

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
        
    }
}
