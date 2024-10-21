using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HugeDoor : MonoBehaviour
{

    [Header("Chests")]
    [SerializeField]
    ChestInteractable[] chestCovers;

    [Header("Key Hole")]
    [SerializeField]
    KeyHoleSocket keyHole;

    [Header("Open Event")]
    [SerializeField]
    UnityEvent openEvent;

    bool isOpened = false;

    private void Start() {

        keyHole.keyInsertedEvent.AddListener(CheckSolved);

        foreach (ChestInteractable _chest in chestCovers) {

            _chest.OpenEvent.AddListener(CheckSolved);
            _chest.closeEvent.AddListener(CheckSolved);

        }

    }

    void CheckSolved() {

        if (!isOpened && keyHole.IsKeyInserted) {

            foreach (ChestInteractable _chest in chestCovers) {

                if (_chest.IsOpened) return;

            }

            openEvent?.Invoke();
            isOpened = true;

        }

    }

}
