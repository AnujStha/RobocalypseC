using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIObjects_Ranged : AIObjects
{
    [Header("Movement")]


    public float targetSmoother;
    [Range(-1, 1)]
    public float targetOffset;
    protected float TargetAngle(Vector3 piviot,Vector3 target)
    {
        float angle = Vector3.Angle(target - piviot, new Vector3(0, 0, 1));
        if (movingPositive)
        {
            if (target.y < piviot.y)
            {
                angle *= -1f;
            }
            angle += 90;
            angle /= 180;
        }
        else
        {
            angle -= 90;
            angle /= 90;
            angle = 1 - angle;
            if (target.y < piviot.y)
            {
                angle *= -1f;
            }
            angle /= 2;
            angle += 0.5f;
        }
        return angle;
    }
}
