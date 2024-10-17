using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PuzzleArea : MonoBehaviour {

    [Header("Right Ray Interactor")]
    [SerializeField]
    ActionBasedControllerManager originRightActBasedControllerManager;
    [SerializeField]
    ActionBasedControllerManager puzzleRoomRightActBasedControllerManager;
    [SerializeField]
    GameObject originRayInteractorObjR;
    [SerializeField]
    GameObject puzzleRoomRayInteractorObjR;

    [Header("Left Ray Interactor")]
    [SerializeField]
    ActionBasedControllerManager originLeftActBasedControllerManager;
    [SerializeField]
    ActionBasedControllerManager puzzleRoomLeftActBasedControllerManager;
    [SerializeField]
    GameObject originRayInteractorObjL;
    [SerializeField]
    GameObject puzzleRoomRayInteractorObjL;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player") {

            originRightActBasedControllerManager.enabled = false;
            puzzleRoomRightActBasedControllerManager.enabled = true;

            originRayInteractorObjR.SetActive(false);
            puzzleRoomRayInteractorObjR.SetActive(true);


            originLeftActBasedControllerManager.enabled = false;
            puzzleRoomLeftActBasedControllerManager.enabled = true;

            originRayInteractorObjL.SetActive(false);
            puzzleRoomRayInteractorObjL.SetActive(true);

        }

    }

    private void OnTriggerExit(Collider other) {

        if (other.gameObject.tag == "Player") {

            originRightActBasedControllerManager.enabled = true;
            puzzleRoomRightActBasedControllerManager.enabled = false;

            originRayInteractorObjR.SetActive(true);
            puzzleRoomRayInteractorObjR.SetActive(false);


            originLeftActBasedControllerManager.enabled = true;
            puzzleRoomLeftActBasedControllerManager.enabled = false;

            originRayInteractorObjL.SetActive(true);
            puzzleRoomRayInteractorObjL.SetActive(false);

        }

    }


}
