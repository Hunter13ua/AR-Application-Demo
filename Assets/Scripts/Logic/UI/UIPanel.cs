using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    public virtual UIStateManager.UIState PanelState { get; protected set; }

    public abstract void Show();
    public abstract void Hide();
    public abstract void Initialize();
}
