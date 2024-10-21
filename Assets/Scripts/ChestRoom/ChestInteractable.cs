using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ChestInteractable : XRBaseInteractable {

    [Header("Chest Open Axis")]
    [SerializeField]
    Transform axis;

    [SerializeField]
    [Range(90, 270)]
    float maxOpenAxis;

    [SerializeField]
    float criticalAngle;

    [SerializeField]
    [Range(0, 1)]
    float openSpeedDelta;

    float curRot = 0;
    float destRot = 0;
    float otherHalfAngle;

    [Header("Events")]
    [SerializeField]
    public UnityEvent OpenEvent;
    [SerializeField]
    public UnityEvent closeEvent;

    public bool IsOpened {  get; private set; }

    Coroutine chestOpenCoroutine = null;

    IXRSelectInteractor interactor;

    private void Start() {
        
        IsOpened = false;

    }

    protected override void OnEnable() {

        selectEntered.AddListener(OnGrabbed);
        selectExited.AddListener(ExitGrabbed);
        base.OnEnable();

        otherHalfAngle = (360 - maxOpenAxis) / 2;

    }

    protected override void OnDisable() {

        selectEntered.RemoveListener(OnGrabbed);
        selectExited.RemoveListener(ExitGrabbed);
        base.OnDisable();

    }

    protected override void OnHoverExited(HoverExitEventArgs args) {

        base.OnHoverExited(args);

        if (isSelected) {

            interactionManager.SelectExit(firstInteractorSelecting, this);

        }

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase) {

        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic) {

            if (isSelected) {

                UpdateState();

            }

        }
    }


    void OnGrabbed(SelectEnterEventArgs _args) {

        interactor = _args.interactorObject;

        if (chestOpenCoroutine != null) { 
        
            StopCoroutine(chestOpenCoroutine);
            chestOpenCoroutine = null;

        }

    }

    void ExitGrabbed(SelectExitEventArgs _args) {

        interactor = null;

        if (curRot < criticalAngle) {
            IsOpened = false;
            closeEvent?.Invoke();
            destRot = 0;
        } else {
            IsOpened = true;
            OpenEvent?.Invoke();
            destRot = maxOpenAxis;
        }

        if (chestOpenCoroutine == null)
            chestOpenCoroutine = StartCoroutine(SetCoverPosCO());

    }

    void UpdateState() {

        Vector3 dir = interactor.GetAttachTransform(this).position - transform.position;
        dir = transform.InverseTransformDirection(dir);
        dir.z = 0;

        curRot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (curRot < 0) curRot += 360;

        if (curRot > maxOpenAxis) {

            if (curRot - maxOpenAxis < otherHalfAngle) {
                curRot = maxOpenAxis;
            } else {
                curRot = 0;
            }

        }


        axis.transform.localRotation = Quaternion.Euler(0, 0, curRot);

    }

    IEnumerator SetCoverPosCO() {


        while (Mathf.Abs(curRot - destRot) > 0.5f) {

            curRot = Mathf.Lerp(curRot, destRot, openSpeedDelta);
            axis.transform.localRotation = Quaternion.Euler(0, 0, curRot);
            yield return null;

        }

        axis.transform.localRotation = Quaternion.Euler(0, 0, destRot);

    }

}
