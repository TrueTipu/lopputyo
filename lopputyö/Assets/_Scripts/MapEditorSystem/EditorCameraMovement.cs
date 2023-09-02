using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class EditorCameraMovement : MonoBehaviour
{
    Camera cam => Helpers.Camera;

    Vector3 dragOrigin;

    [SerializeField] float zoomStep;
    [SerializeField] float minSize;
    [SerializeField] float maxSize;
    
    // Update is called once per frame
    void Update()
    {
        PanCamera();

        Zoom();
    }

    void PanCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 _difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position += _difference;
        }
        
    }
    void Zoom()
    {
        if (Input.GetMouseButton(1)) return;

        Vector3 _mouseOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.mouseScrollDelta.y < 0)
        {
            ZoomOut();
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            ZoomIn();
        }

        Vector3 _difference = _mouseOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
        cam.transform.position += _difference;
    }

    void ZoomIn()
    {
        float _newSize = cam.orthographicSize - zoomStep;
        cam.orthographicSize = Mathf.Clamp(_newSize, minSize, maxSize);
    }


    void ZoomOut()
    {
        float _newSize = cam.orthographicSize + zoomStep;
        cam.orthographicSize = Mathf.Clamp(_newSize, minSize, maxSize);
    }
}
