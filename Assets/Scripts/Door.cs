using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Editor;

public class Door : MonoBehaviour {

    [SerializeField]
    float openDelta = -90;

    Quaternion startRot;

    private void Awake() {

        startRot = transform.rotation;

    }

    public void OpenDoor() {

        StartCoroutine(OpenDoorCo());

    }

    IEnumerator OpenDoorCo() {

        float time = 0;
        Quaternion destRot = startRot * Quaternion.Euler(0, openDelta, 0);

        while (time < 1) {

            transform.rotation = Quaternion.Lerp(startRot, destRot, time);
            //transform.rotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, new Vector3(0, -90, 0), time));
            time += Time.deltaTime / 2;
            yield return null;

        }


    }

}
