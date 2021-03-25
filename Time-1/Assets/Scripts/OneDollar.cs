using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static System.Math;

public class OneDollar
{
    public static List<List<float>> Result(List<List<float>> points) {

        List<List<float>> result = Resample(points, 64);

        List<List<float>> result2 = RotateToZero(result);
        

        return result2;

    }

    //Step 1
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

    //Step 2
    public static List<List<float>> RotateToZero(List<List<float>> points) {
        List<float> centroid = Centroid(points);
        float theta = (float)Atan2(centroid[1]-points[0][1], centroid[0]-points[0][0]);
        List<List<float>> newPoints = RotateBy(points, -theta);
        return newPoints;
    }

    public static List<List<float>> RotateBy(List<List<float>> points, float theta) {
        List<float> centroid = Centroid(points);
        List<List<float>> newPoints = new List<List<float>>();
        float cos = (float)Cos(theta);
        float sin = (float)Sin(theta);
        foreach (var point in points) {
            float qx = (point[0]-centroid[0]) * cos - (point[1]-centroid[1]) * sin+ centroid[0];
            float qy = (point[0]-centroid[0]) * sin - (point[1]-centroid[1]) * cos + centroid[1];
            newPoints.Add(new List<float>{qx, qy});
        }
        return newPoints;
    }

    public static List<float> Centroid(List<List<float>> points) {
        List<float> centroid = new List<float>();
        centroid.Add(0);
        centroid.Add(0);
        for (int i = 0; i < points.Count; i++) {
            centroid[0] += points[i][0];
            centroid[1] += points[i][1];
        }
        centroid[0] = centroid[0] / points.Count;
        centroid[1] = centroid[1] / points.Count;
        return centroid;
    }
    

}
