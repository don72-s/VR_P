using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Button : XRBaseInteractable
{

    [SerializeField]
    Transform button_Button;
    [SerializeField]
    Transform baseTransform;

    [SerializeField]
    float offsetY;

    [Header("Pushed Event")]
    [SerializeField]
    UnityEvent pushedEvent;

    bool isInteracting = false;
    IXRHoverInteractor interactor;

    bool isPushed = false;

    public override bool IsHoverableBy(IXRHoverInteractor interactor) {
        if (interactor is XRRayInteractor)
            return false;
        return base.IsHoverableBy(interactor);
    }

    protected override void OnEnable() {

        base.OnEnable();

        hoverEntered.AddListener(StartHover);
        hoverExited.AddListener(EndHover);

    }

    protected override void OnDisable() {

        hoverEntered.RemoveListener(StartHover);
        hoverExited.RemoveListener(EndHover);

        base.OnDisable();

    }

    void StartHover(HoverEnterEventArgs args) {

        if (isInteracting || isPushed)
            return;
        interactor = args.interactorObject;
        isInteracting = true;

    }

    void EndHover(HoverExitEventArgs args) {

        if (!isInteracting || isPushed)
            return;

        isInteracting = false;

        interactor = null;

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase) {

        base.ProcessInteractable(updatePhase);

        if (isPushed)
            return;

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic) {

            if (isInteracting) {
                UpdatePress();
            }

        }
    }

    void UpdatePress() {

        Transform interactorTransform = interactor.GetAttachTransform(this);
        Vector3 after = baseTransform.InverseTransformVector(interactorTransform.position - baseTransform.position);

        after.y = Mathf.Clamp(after.y + offsetY, -1, 0) / 10;

        if (after.y < -0.09999f) {

            after.y = -0.1f;
            isPushed = true;
            pushedEvent?.Invoke();

        }

        button_Button.transform.localPosition = new Vector3(button_Button.transform.localPosition.x,
                                                after.y,
                                                button_Button.transform.localPosition.z);


    }


}
