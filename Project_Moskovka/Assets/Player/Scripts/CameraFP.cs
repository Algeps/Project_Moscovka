using Cinemachine;
using UnityEngine;

public class CameraFP : MonoBehaviour
{
    private static CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public static void FollowPlayer(Transform transform)
    {
        // not all scenes have a cinemachine virtual camera so return in that's the case
        if (cinemachineVirtualCamera == null) return;
        cinemachineVirtualCamera.Follow = transform;
    }
}
