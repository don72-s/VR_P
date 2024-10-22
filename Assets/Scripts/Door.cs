using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Editor;

public class Door : MonoBehaviour {

    [SerializeField]
    float openDelta = -90;

    [SerializeField]
    UnityEvent onOpenEvent;

    Quaternion startRot;

    bool isOpened = false;

    private void Awake() {

        startRot = transform.rotation;

    }

    public void OpenDoorOneTime() {

        if (!isOpened) {

            isOpened = true;
            OpenDoor();

        }
    
    }

    public void OpenDoor() {

        StartCoroutine(OpenDoorCo());
        onOpenEvent?.Invoke();

    }

    IEnumerator OpenDoorCo() {

        float time = 0;
        Quaternion destRot = startRot * Quaternion.Euler(0, openDelta, 0);

        while (time < 1) {

            transform.rotation = Quaternion.Lerp(startRot, destRot, time);
            time += Time.deltaTime / 2;
            yield return null;

        }


    }

}
