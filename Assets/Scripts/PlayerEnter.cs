using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEnter : MonoBehaviour
{

    [SerializeField]
    UnityEvent OnEnterEvent;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player") {
            OnEnterEvent?.Invoke();

        }

    }
}
