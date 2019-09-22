using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMath
{
    public const float GConstant = 0.005f;
    
    public static Vector2 GetVelocityForCircularOrbitAtRadius(Vector2 rVector, float mass)
    {
        var mu = mass * GConstant;
         
        var v = Mathf.Sqrt(mu / rVector.magnitude);
        
        var tangent = new Vector2(rVector.y, -rVector.x).normalized;
        return tangent * v;
    }
}
