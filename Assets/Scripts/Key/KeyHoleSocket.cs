using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyHoleSocket : XRSocketInteractor{

    protected override void Start() {

        base.Start();
        selectEntered.AddListener(OnEntered);

    }

    public override bool CanHover(IXRHoverInteractable interactable) {

        return interactable.transform.gameObject.tag == "Key" && base.CanHover(interactable);

    }

    public override bool CanSelect(IXRSelectInteractable interactable) {

        return interactable.transform.gameObject.tag == "Key" && base.CanSelect(interactable);

    }

    void OnEntered(SelectEnterEventArgs _args) { 
    
        _args.interactableObject.transform.GetComponent<Collider>().enabled = false;

    }

}
