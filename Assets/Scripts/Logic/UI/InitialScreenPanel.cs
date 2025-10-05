public class InitialScreenPanel : UIPanel
{
    public override UIStateManager.UIState PanelState => UIStateManager.UIState.InitialScreen;

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
