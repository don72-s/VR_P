using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour {

    [SerializeField]
    TilePosFitter[] tiles;

    [SerializeField]
    UnityEvent puzzleClearAction;

    public void Start() {

        foreach (TilePosFitter _tile in tiles) {

            _tile.puzzlePlaced += CheckPuzzleClear;

        }

        

    }

    void CheckPuzzleClear() {

        foreach (TilePosFitter _tile in tiles) {

            if (!_tile.CheckCorrect())
                return;

        }

        Debug.Log("Clear!");

        puzzleClearAction?.Invoke();

        foreach (TilePosFitter _tile in tiles) {

            _tile.puzzlePlaced -= CheckPuzzleClear;

        }

    }

}
