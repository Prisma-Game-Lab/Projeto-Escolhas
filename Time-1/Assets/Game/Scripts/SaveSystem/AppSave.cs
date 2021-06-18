using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "Save/Scriptable Obj")]
public class AppSave : ScriptableObject
{
    public List<string> elfa = new List<string>();
    public List<string> humano = new List<string>();
    public List<string> sereia = new List<string>();
    public List<string> orc = new List<string>();
    public string elfaJson;
    public string humanoJson;
    public string sereiaJson;
    public string orcJson;
    public List<CharacterBase> tinderMatched = new List<CharacterBase>();
}
