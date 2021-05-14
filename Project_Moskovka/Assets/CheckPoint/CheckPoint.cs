using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameObject checkpoint;
    public float time_destroyer;
    public float time_destroy = 5;


    void Start()
    {
       // Instantiate (checkpoint);
        
    }


    void Update()
    {
        time_destroy+=Time.deltaTime;
        if(time_destroyer > time_destroy){
            Destroy(gameObject);
        }
    }

   private void OnTriggerEnter3D (Collider collision)
    {
        if(collision.CompareTag ("Player"))
        {
            Destroy(gameObject);
        }

    }
}
