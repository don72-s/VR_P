using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(TeleportationAnchor))]
public class ChessTile : MonoBehaviour {

    TeleportationAnchor teleportationAnchor;
    Renderer render;
    Material originMaterial;

    public bool IsVisited {  get; private set; }
    public event Action OnVisitEvent;

    [SerializeField]
    Material visitedMaterial;

    [Header("Movable Tile")]
    [SerializeField]
    ChessTile[] movableTile;
    [SerializeField]
    GameObject displayModel;

    private void Awake() {

        teleportationAnchor = GetComponent<TeleportationAnchor>();
        render = GetComponent<Renderer>();
        originMaterial = render.material;

        IsVisited = false;
        displayModel.SetActive(false);

    }

    public void OnTeleported(TeleportingEventArgs _args) { 
    
        render.material = visitedMaterial;
        IsVisited = true;
        OnVisitEvent?.Invoke();

        foreach (ChessTile _tile in movableTile) {

            if (!_tile.IsVisited) {

                _tile.Teleportable(true);
                _tile.ModelDisplay(true);

            }

        }

    }

    public void ResetVisit() {

        render.material = originMaterial;
        IsVisited = false;

    }

    public void Teleportable(bool _isAble) {

        teleportationAnchor.enabled = _isAble;

    }

    public void ModelDisplay(bool _isVisible) {

        displayModel.SetActive(_isVisible);

    }


}
