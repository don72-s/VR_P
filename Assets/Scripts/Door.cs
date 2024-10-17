using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Door : MonoBehaviour {

    public void OpenDoor() {

        StartCoroutine(OpenDoorCo());

    }

    IEnumerator OpenDoorCo() {

        float time = 0;


        while (time < 1) {

            transform.rotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, new Vector3(0, -90, 0), time));
            time += Time.deltaTime / 2;
            yield return null;

        }


    }

}
