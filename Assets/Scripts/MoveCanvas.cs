using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    private Vector3 ActivePosition = new Vector3(2.3f, 2.3f, .45f);
    private Vector3 InactivePosition = new Vector3(1000f, 2.3f, .45f);

    public void IsActive(Boolean active)
    {
        if (active)
        {
            canvas.transform.position = InactivePosition;
        }
        else
        {
            canvas.transform.position = ActivePosition;
        }
    }
}
