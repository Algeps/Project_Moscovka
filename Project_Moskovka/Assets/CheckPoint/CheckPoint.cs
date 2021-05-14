using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("PickupObject")]

    [Tooltip("New Gameobject (a VFX for example) to spawn when you trigger this PickupObject")]
    public GameObject spawnPrefabOnPickup;

    [Tooltip("Destroy the spawned spawnPrefabOnPickup gameobject after this delay time. Time is in seconds.")]
    public float destroySpawnPrefabDelay = 10;
    
    [Tooltip("Destroy this gameobject after collectDuration seconds")]
    public float collectDuration = 0f;
     public Transform CollectVFXSpawnPoint;

    LayerMask Collisionmask;

    public GameObject checkpoint;
    public delegate void Action<in T>(T obj);
    public static Action<CheckPoint> OnRegisterPickup;

    public static Action<CheckPoint> OnUnregisterPickup;
    
    public float time_destroy = 5;

    void Start()
    {
       // Instantiate (checkpoint);
        
    }


    void Update()
    {
        
        CheckPoint.OnRegisterPickup?.Invoke(this);
    
    }

    void  OnCollect()
    {
        if (spawnPrefabOnPickup)
        {
            var vfx = Instantiate(spawnPrefabOnPickup, CollectVFXSpawnPoint.position, Quaternion.identity);
            Destroy(vfx, destroySpawnPrefabDelay);
        }

        CheckPoint.OnUnregisterPickup(this);

        Destroy (gameObject, collectDuration);
    }

   void OnTriggerEnter(Collider other)
    {
        if ((Collisionmask.value & 1 << other.gameObject.layer) > 0 && other.gameObject.CompareTag("Player"))
        {
            OnCollect();
        }
    }
}
