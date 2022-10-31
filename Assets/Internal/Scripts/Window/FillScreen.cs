using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillScreen : MonoBehaviour
{
    ///////////////////////////////
    //  PRIVATE VARIABLES         //
    ///////////////////////////////
    private Resolution _res;
    private Camera _cam;

    ///////////////////////////////
    //  PRIVATE METHODS           //
    ///////////////////////////////
    private void Start()
    {
        _res = Screen.currentResolution;
        _cam = Camera.main;
        ObjectFillScreen();
        transform.parent = _cam.transform;

    }

    private void Update()
    {
        if (_res.width != Screen.width || _res.height != Screen.height)
        {
            ObjectFillScreen();
        }  
    }

    private void ObjectFillScreen()
    {

        float pos = (_cam.nearClipPlane + 0.01f);

        transform.position = _cam.transform.position + _cam.transform.forward * pos;
        transform.LookAt(_cam.transform);
        transform.Rotate(90.0f, 0.0f, 0.0f);

        float h = (Mathf.Tan(_cam.fieldOfView * Mathf.Deg2Rad * 0.5f) * pos * 2f) / 10.0f;

        transform.localScale = new Vector3(h * _cam.aspect, 1.0f, h * _cam.aspect);
    }
}
