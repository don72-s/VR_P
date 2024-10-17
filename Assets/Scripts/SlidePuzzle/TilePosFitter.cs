using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class TilePosFitter : MonoBehaviour {

    [SerializeField]
    GameObject correctPuzzleTile;

    XRSocketInteractor interactor;

    bool isCorrect;
    public event Action puzzlePlaced;

    private void Awake() {
        
        interactor = GetComponent<XRSocketInteractor>();
        isCorrect = (interactor == null);

    }

    private void OnEnable() {

        interactor.selectEntered.AddListener(OnEnter);
        interactor.selectExited.AddListener(OnExit);

    }

    private void OnDisable() {

        interactor.selectEntered.RemoveListener(OnEnter);
        interactor.selectExited.RemoveListener(OnExit);

    }

    public bool CheckCorrect() {

        return isCorrect;

    }

    void OnEnter(SelectEnterEventArgs _args) {


        isCorrect = _args.interactableObject.transform.gameObject == correctPuzzleTile;

        _args.interactableObject.transform.gameObject.GetComponent<Tile>().SetBaseParent();

        puzzlePlaced?.Invoke();


    }

    void OnExit(SelectExitEventArgs _args) { 
    
        isCorrect = false;

        if (correctPuzzleTile == null)
            isCorrect = true;

    }

}
