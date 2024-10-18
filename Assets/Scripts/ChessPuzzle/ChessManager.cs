using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ChessManager : MonoBehaviour {

    [SerializeField]
    ChessTile[] chessTiles;
    [SerializeField]
    GameObject resetAreas;

    [Space(20)]
    [SerializeField]
    UnityEvent puzzleClearAction;

    bool isClear = false;
    bool gameStarted = false;

    private void Start() {

        foreach (ChessTile _tile in chessTiles) {

            _tile.OnVisitEvent += TileTeleportCallback;

        }

        resetAreas.SetActive(false);

    }

    public void OnTeleported(TeleportingEventArgs _args) {

        if (!isClear) {

            gameStarted = false;

            foreach (ChessTile _tile in chessTiles) {
            
                _tile.ResetVisit();
                _tile.Teleportable(true);
                _tile.ModelDisplay(false);

            }

        }

        resetAreas.SetActive(false);


    }

    void TileTeleportCallback() {

        if (isClear)
            return;

        if (!gameStarted) {

            gameStarted = true;
            resetAreas.SetActive(true);

        }

        foreach (ChessTile _tile in chessTiles) {

            _tile.Teleportable(false);
            _tile.ModelDisplay(false);

        }

        foreach (ChessTile _tile in chessTiles) {

            if (!_tile.IsVisited)
                return;

        }

        OnClear();

    }

    void OnClear() { 
    
        isClear = true;
        resetAreas.SetActive(false);

        puzzleClearAction?.Invoke();

        Debug.Log("clear!");

    }

}
