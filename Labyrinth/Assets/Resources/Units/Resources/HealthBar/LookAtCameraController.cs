using UnityEngine;

public class LookAtCameraController : MonoBehaviour
{
    public Camera main_camera;

    void Update()
    {
        transform.LookAt(transform.position + main_camera.transform.rotation * Vector3.back,
            main_camera.transform.rotation * Vector3.down);
    }
}
