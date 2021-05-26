using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public string _version = "0.1 alfa";
    public Vector2 vVersion;
    public Font versionFont;
    public byte sizeText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    //������ �� ������
    void OnGUI ()
    {
        GUI.skin.font = versionFont;
        GUI.skin.label.fontSize = sizeText;
        GUI.Label(new Rect(vVersion.x, vVersion.y, 250, 150), "version: "+_version);
    }
}
