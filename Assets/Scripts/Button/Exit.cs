using Unity.XR.Oculus.Input;
using UnityEngine;

public class Exit : MonoBehaviour {

    public void ExitGame() {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif

    }

}
