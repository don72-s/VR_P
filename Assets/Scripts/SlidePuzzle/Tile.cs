using UnityEngine;

public class Tile : MonoBehaviour {

    Transform baseParent;

    private void Awake() {

        baseParent = transform.parent;

    }

    public void SetBaseParent() { 
    
        transform.SetParent(baseParent);

    }

}
