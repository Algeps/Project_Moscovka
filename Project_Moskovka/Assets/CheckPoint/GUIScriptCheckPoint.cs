using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIScriptCheckPoint : MonoBehaviour
{
    public int iNumberOfRings = 0;
    public Vector2 vectorNumberOfRings;
    public Font fontNumberOfRings;
    public byte sizeText;

    public float timeRezultCheckPoint = 5;

    void FixedUpdate() 
    {
        if (iNumberOfRings == 8)
        {
            if(timeRezultCheckPoint > 0) timeRezultCheckPoint -= Time.deltaTime;
            if(timeRezultCheckPoint < 0) timeRezultCheckPoint = 0;
        }
    }
    
    void OnGUI ()
    {
        if(iNumberOfRings > 0 && iNumberOfRings < 8)
        {
            GUI.skin.font = fontNumberOfRings;
            GUI.skin.label.fontSize = sizeText;
            GUI.Label(new Rect(vectorNumberOfRings.x, vectorNumberOfRings.y, 300, 450), "Количество пройдённых колец: "+ iNumberOfRings.ToString());
        }
        else if(iNumberOfRings == 8 && timeRezultCheckPoint != 0)
        {
            GUI.skin.font = fontNumberOfRings;
            GUI.skin.label.fontSize = sizeText;
            GUI.Label(new Rect(vectorNumberOfRings.x, vectorNumberOfRings.y, 450, 150), "Молодец! Ты собрал все 8 колец! ");
        }   
    }
    
    //убирать монетки
    private void OnTriggerEnter (Collider other)//функция, которая отвечает за прохождение сквозь предметы
        {
        if(other.gameObject.tag == "_checkpoint")
        {
            Destroy(other.gameObject);
            
        }
        iNumberOfRings++;
    }
}
