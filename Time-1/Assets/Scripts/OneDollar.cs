using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static System.Math;

public class OneDollar
{
    
    public static List<List<float>> Resample(List<List<float>> points, int n) {
        float I = PathLength(points)/(n-1);
        float D = 0;
        List<List<float>> newPoints = new List<List<float>>();
        newPoints.Add(points[0]);
        for (int i = 1; i < points.Count; i++) {
            float d = Distance(points[i-1], points[i]);
            if ((D+d) >= I) {
                float qx = points[i-1][0] + (((I-D)/d) * (points[i][0] - points[i-1][0]));
                float qy = points[i-1][1] + (((I-D)/d) * (points[i][1] - points[i-1][1]));
                newPoints.Add(new List<float>{qx, qy});
                points.Insert(i, new List<float>{qx, qy});
                D = 0;
            }
            else {
                D += d;
            }
        }
        return newPoints;
    }

    public static float PathLength(List<List<float>> A) {
        float d = 0;
        for (int i = 1; i < A.Count; i++) {
            d += Distance(A[i-1], A[i]);
        }
        return d;
    }

    public static float Distance(List<float> A1, List<float> A2) {
        float x = A2[0]-A1[0];
        float y = A2[1]-A1[1];
        float d = (float)Sqrt(Pow(x,2) + Pow(y,2));
        return d;
    }
    

}
