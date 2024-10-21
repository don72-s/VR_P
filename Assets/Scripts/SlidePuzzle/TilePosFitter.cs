using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor), typeof(AudioSource))]
public class TilePosFitter : MonoBehaviour {

    public static float sfxVolume = 0;

    [SerializeField]
    GameObject correctPuzzleTile;

    XRSocketInteractor interactor;
    AudioSource audioSource;

    bool isCorrect;
    public event Action puzzlePlaced;

    private void Awake() {
        
        interactor = GetComponent<XRSocketInteractor>();
        audioSource = GetComponent<AudioSource>();
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

        audioSource.volume = sfxVolume;
        audioSource.Play();

    }

    void OnExit(SelectExitEventArgs _args) { 
    
        isCorrect = false;

        if (correctPuzzleTile == null)
            isCorrect = true;

    }

}
