using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Vector3 offset;
    private Transform Target;
   [Range(0, 1)]public float lerpValue;

    public void Start()
    {
        Target = GameObject.Find("Player").transform;
    }
    public void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + offset, lerpValue);
        transform.LookAt(Target);
    }
}
