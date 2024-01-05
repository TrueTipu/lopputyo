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

    private void Update()
    {
        if(Mathf.Round(Vector2.Distance(transform.position, target.position)*100) != 0)
        {
            var _a = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime*speed* Vector2.Distance(transform.position, target.position));
            transform.position = new Vector3(_a.x, _a.y, -10);
        }
    }
}
