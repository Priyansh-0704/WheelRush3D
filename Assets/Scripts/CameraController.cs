using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private float offsetZ = 8f;

    void LateUpdate()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, target.position.z - offsetZ);
        transform.position = Vector3.Lerp(transform.position, newPos, 10 * Time.deltaTime);
    }
}
