using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static System.Math;

public class OneDollar
{
    
    public static float Resample(List<List<float>> points, int n) {
        float I = PathLength(points)/(n-1);
        return I;
    }

    public static float PathLength(List<List<float>> A) {
        float d = 0;
        for  (int i = 1; i < A.Count; i++) {
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
