using UnityEngine;

/// <summary>
/// Author: Frank01001
/// https://github.com/Frank01001/Unity---HL-A-Gravity-Gloves-Basic-Implementation
/// </summary>

public class RATSCalculations
{
    /* Calculations for a parabolic movement */
    public static Vector3 CalculateParabola(Vector3 start, Vector3 vertex)
    {
        //Gittata
        float half_range = Mathf.Sqrt((start.x - vertex.x)*(start.x - vertex.x) + (start.z - vertex.z)*(start.z - vertex.z));
        //Parabola's parameters
        float a, c;
        //Angle of parabolic movement
        float angle;
        //Buffers
        float velocity_module;
        Vector3 velocity_vers;
        Quaternion rotation_buffer;

        //Calculation of parabola's parameters
        c = vertex.y - start.y;
        a = -c / (half_range * half_range);
        //Calculation of motion angle through derivative
        angle = Mathf.Atan(2*a* half_range);

        //Module of velocity vector trough parabolic movement formulas
        velocity_module = Mathf.Sqrt(2.0f * half_range * 9.81f / Mathf.Abs(Mathf.Sin(2.0f * angle)));

        //Velocity Versor (unit vector)
        velocity_vers = vertex - start;
        velocity_vers.y = 0.0f;
        velocity_vers.Normalize();
        rotation_buffer = Quaternion.AngleAxis(angle*180.0f/3.1415f, Vector3.Cross(velocity_vers, Vector3.up));

        velocity_vers = rotation_buffer * velocity_vers;

        //Store result
        return velocity_vers * velocity_module + Vector3.up * 9.80665f;
    }

    public static Vector3 CalculateMidpoint(Transform a, Transform b)
    {
        // Transform midpoint = Transform(Vector3.Zero, Vector3.Zero, Vector3.Zero);
        //
        // Vector3 directionCtoA = a.position - transform.position; // directionCtoA = positionA - positionC
        // Vector3 directionCtoB = OtherObjectB.position - transform.position; // directionCtoB = positionB - positionC
        // Vector3 midpointAtoB = new Vector3((directionCtoA.x+directionCtoB.x)/2.0f,(directionCtoA.y+directionCtoB.y)/2.0f,(directionCtoA.z+directionCtoB.z)/2.0f); // midpoint between A B this is what you want
        
        return (a.position + b.position) * 0.5f;
    }
    
    public static Vector3 CalculateQuadraticBezierCurves(Vector3 start, Vector3 peak, Vector3 target, float t)
    {
        // throw new System.NotImplementedException();
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u; // (1-t)^2
        
        Vector3 p = uu * start; 
        p += 2 * u * t * peak; 
        p += tt * target; 
        
        return p;
    }
    
    // Taken from: https://www.gamasutra.com/blogs/VivekTank/20180806/323709/How_to_work_with_Bezier_Curve_in_Games_with_Unity.php
    public Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        
        Vector3 p = uuu * p0; 
        p += 3 * uu * t * p1; 
        p += 3 * u * tt * p2; 
        p += ttt * p3; 
        
        return p;
    }
}