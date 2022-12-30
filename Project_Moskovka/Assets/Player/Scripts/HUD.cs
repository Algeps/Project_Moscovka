using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public string _version = "0.2 alfa";
    public Vector2 vVersion;
    public Font versionFont;
    public byte sizeText;

    void OnGUI ()
    {
        GUI.skin.font = versionFont;
        GUI.skin.label.fontSize = sizeText;
        GUI.Label(new Rect(vVersion.x, vVersion.y, 250, 150), "Version: "+_version);
    }
}
