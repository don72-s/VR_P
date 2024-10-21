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

    [Header("Lock Action Gestures")]
    [SerializeField]
    GameObject moveLocomotion;
    [SerializeField]
    GameObject grabMoveLocomotion;
    [SerializeField]
    GameObject climbLocomotion;

    bool moveState;
    bool grabMoveState;
    bool climbState;

    bool isClear = false;
    bool gameStarted = false;

    private void Start() {

        foreach (ChessTile _tile in chessTiles) {

            _tile.OnVisitEvent += TileTeleportCallback;

        }

        resetAreas.SetActive(false);

    }

    /// <summary>
    /// 텔레포트 이외의 모든 입력 행동 제한
    /// </summary>
    void LockOtherActions() {

        moveState = moveLocomotion.activeSelf;
        grabMoveState = grabMoveLocomotion.activeSelf;
        climbState = climbLocomotion.activeSelf;

        moveLocomotion.SetActive(false);
        grabMoveLocomotion.SetActive(false);
        climbLocomotion.SetActive(false);

    }

    /// <summary>
    /// 행동제한 해제
    /// </summary>
    void ReleaseOtherActions() {

        moveLocomotion.SetActive(moveState);
        grabMoveLocomotion.SetActive(grabMoveState);
        climbLocomotion.SetActive(climbState);
    }

    public void OnTeleported(TeleportingEventArgs _args) {

        if (!isClear) {

            ReleaseOtherActions();

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
            LockOtherActions();
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
        ReleaseOtherActions();

        puzzleClearAction?.Invoke();

    }

}
