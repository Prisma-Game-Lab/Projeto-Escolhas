using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static System.Math;
using System.IO;
using SimpleJSON;

public class OneDollar
{
    public static List<List<float>> Result(List<List<float>> points) {
        List<List<float>> result = Resample(points, 64);
        List<List<float>> result2 = RotateToZero(result);
        List<List<float>> result3 = ScaleToSquare(result2, 256.0f);
        List<List<float>> result4 = TranslateToOrigin(result3);
        //CreateTemplates(result4, "square");
        Recognize(result4);

        return result4;
    }

    public static void CreateTemplates(List<List<float>> step3, string name) {
        string path = @"C:\Users\tatir\OneDrive\Documentos\Test.txt";
        using (StreamWriter sw = File.CreateText(path)) {
            sw.WriteLine("{");
            sw.Write(name);
            sw.WriteLine("[");
            for (int i = 0; i < step3.Count; i++) {
                sw.WriteLine("{");
                sw.Write("x: ");
                sw.Write(step3[i][0]);
                sw.WriteLine(";");
                sw.Write("y: ");
                sw.WriteLine(step3[i][1]);
                sw.WriteLine("};");
            }
            sw.WriteLine("]");
            sw.WriteLine("}");
        }
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
            float qx = (point[0]-centroid[0]) * cos - (point[1]-centroid[1]) * sin + centroid[0];
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

    //Step 3
    public static List<List<float>> ScaleToSquare(List<List<float>> points, float size) {
        List<List<float>> B = BoundingBox(points);
        List<List<float>> newPoints = new List<List<float>>();
        float width = Abs(B[1][0]-B[0][0]);
        float height = Abs(B[1][1]-B[0][1]);
        foreach (var point in points) {
            float qx = point[0] * (size/width);
            float qy = point[1] * (size/height);
            newPoints.Add(new List<float>{qx, qy});
        }
        return newPoints;
    }

    public static List<List<float>> BoundingBox(List<List<float>> points) {
        List<List<float>> B = new List<List<float>>();
        float minx = points[0][0];
        float miny = points[0][1];
        float maxx = minx;
        float maxy = miny;
        for (int i = 1; i < points.Count; i++) {
            if(points[i][0] < minx)
                minx = points[i][0];
            if(points[i][1] < miny)
                miny = points[i][1];
            if(points[i][0] > maxx)
                maxx = points[i][0];
            if(points[i][1] > maxy)
                maxy = points[i][1];
        }
        B.Add(new List<float>{minx, miny});
        B.Add(new List<float>{maxx, maxy});
        return B;
    }

    public static List<List<float>> TranslateToOrigin(List<List<float>> points) {
        List<float> centroid = Centroid(points);
        List<List<float>> newPoints = new List<List<float>>();
        foreach (var point in points) {
            float qx = point[0] - centroid[0];
            float qy = point[1] - centroid[1];
            newPoints.Add(new List<float>{qx, qy});
        }
        return newPoints;
    }
    
    //Step 4
    public static void Recognize(List<List<float>> points) {
        //float b = float.PositiveInfinity;
        List<List<float>> template = new List<List<float>>();
        string path = @"C:\Users\tatir\OneDrive\Documentos\Templates.json";
        string jsonString = File.ReadAllText (path); 
        JSONNode data = JSON.Parse(jsonString);
        foreach(JSONNode record in data["square"])
        {
            Debug.Log ("x: " + record["x"].Value + "y: " + record["y"].Value);
        }

        /*
        using (StreamReader sw = new StreamReader(path)) {
            string line;
            while ((line = sw.ReadLine()) != null) {
                if (line.Contains("y")) {
                    Debug.Log(line.IndexOf(":"));
                    line = line.Substring(line.IndexOf(":"), line.IndexOf(",")); 
                    Debug.Log(line);
                }
                if (line.Contains("x")) {
                    //Debug.Log(line.Substring(3, line.LastIndexOf(",")));
                }
            }
        }
        */
        //foreach (var template in templates) {

        //}
    }
    
}
