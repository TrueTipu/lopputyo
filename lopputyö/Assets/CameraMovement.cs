using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float speed;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if(transform.position != target.position)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed);
        }
    }
}
