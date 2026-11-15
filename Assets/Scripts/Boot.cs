using UnityEngine;

// Smallest possible proof that a Unity build ran: a tinted screen and one line of text.
public class Boot : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Create()
    {
        new GameObject("Boot").AddComponent<Boot>();
    }

    void Start()
    {
        var cam = Camera.main;
        if (cam == null) cam = new GameObject("Camera").AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.03f, 0.12f, 0.10f);
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.height / 12;
        GUI.Label(new Rect(30, 30, Screen.width - 60, Screen.height / 3),
            "Chasing Shadows\nUnity license test: it works");
    }
}
