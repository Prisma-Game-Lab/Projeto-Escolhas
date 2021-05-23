using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "Save/Scriptable Obj")]
public class AppSave : ScriptableObject
{
    public List<GameObject> elfa;
    public List<GameObject> humano;
    public List<GameObject> sereia;
    public List<GameObject> orc;
}
