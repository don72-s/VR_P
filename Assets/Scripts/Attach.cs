using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Attach : MonoBehaviour {

    IXRSelectInteractable interactable;

    private void Awake() {

        interactable = GetComponent<IXRSelectInteractable>();

    }

    private void OnEnable() {

        if (interactable != null) {

            interactable.selectEntered.AddListener(OnSelected);

        }

    }

    private void OnDisable() {

        interactable.selectEntered.RemoveListener(OnSelected);


    }

    void OnSelected(SelectEnterEventArgs _args) {

        if (!(_args.interactorObject is XRRayInteractor))
            return;

        var attachTransform = _args.interactorObject.GetAttachTransform(interactable);
        var originalAttachPose = _args.interactorObject.GetLocalAttachPoseOnSelect(interactable);
        attachTransform.SetLocalPositionAndRotation(originalAttachPose.position, originalAttachPose.rotation);

    }
}
