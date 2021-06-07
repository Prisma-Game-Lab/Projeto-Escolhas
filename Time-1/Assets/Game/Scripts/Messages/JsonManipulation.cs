using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonManipulation
{
    public static void DuplicateJson() {
        //for (int i = 1; i < 4; i++) {
        File.Copy( "Assets/Game/Ink/Sereia/Dia1.ink.json", "Assets/Resources/Ink Json/Sereia Aux/Dia.ink.json", true);
        //}
        
    }
}
