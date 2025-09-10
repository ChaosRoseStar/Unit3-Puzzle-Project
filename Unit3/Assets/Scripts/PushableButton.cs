using UnityEngine;
using UnityEngine.Events;

public class PushableButton : MonoBehaviour, ISelectable
{
    public UnityEvent OnPush;

    [SerializeField] private MeshRenderer buttonRenderer;

    [Space]
    [SerializeField] private Material offMat;
    [SerializeField] private Material selectedMat;

    public void OnHoverEnter()
    {
        buttonRenderer.material = selectedMat;
    }

    public void OnHoverExit()
    {
        buttonRenderer.material = offMat;
    }

    public void OnSelect()
    {
        OnPush.Invoke();
    }

}
