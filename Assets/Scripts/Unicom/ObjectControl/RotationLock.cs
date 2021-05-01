using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLock : MonoBehaviour
{
    [Header("Lock X Axis")]
    [SerializeField] bool LockX = false;
    [SerializeField] float XLockValue = 0;
    [Header("Lock Y Axis")]
    [SerializeField] bool LockY = false;
    [SerializeField] float YLockValue = 0;
    [Header("Lock Z Axis")]
    [SerializeField] bool LockZ = false;
    [SerializeField] float ZLockValue = 0;

    Vector3 angle = new Vector3(0,0,0);
    void Start()
    {
        angle = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        angle = transform.eulerAngles;
        //Debug.Log(angle);
        if (LockX)
        {
            if (transform.rotation.eulerAngles.x != XLockValue)
            {
                angle.x = XLockValue;
            }
        }
        if (LockY)
        {
            if (transform.rotation.eulerAngles.y != YLockValue)
            {
                angle.y = YLockValue;
            }
        }
        if (LockZ)
        {
            if (transform.rotation.eulerAngles.z != ZLockValue)
            {
                angle.z = ZLockValue;
            }
        }
        transform.eulerAngles = angle;
    }
}
