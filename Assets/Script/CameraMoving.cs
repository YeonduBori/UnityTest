using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public Transform[] pivotTransforms;
    private Vector3 centerPos;
    // Start is called before the first frame update
    void Start()
    {
        centerPos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        CamLookCenter();
    }

    /// <summary>
    /// 카메라가 항상 물체들 포지션의 중점을 바라봅니다.
    /// </summary>
    void CamLookCenter()
    {
        for (int pivotIndex = 0; pivotIndex < pivotTransforms.Length; pivotIndex++)
        {
            centerPos += pivotTransforms[pivotIndex].position;
        }
        centerPos /= pivotTransforms.Length;
        gameObject.transform.LookAt(centerPos);
    }
}
