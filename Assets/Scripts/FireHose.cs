using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHose : MonoBehaviour
{
    [SerializeField] private LineRenderer waterLine;
    [SerializeField] private float lineLength;

    private void Update()
    {
        waterLine.SetPosition(0, transform.position);
        waterLine.SetPosition(1, transform.position + waterLine.transform.forward * lineLength);
    }
}
