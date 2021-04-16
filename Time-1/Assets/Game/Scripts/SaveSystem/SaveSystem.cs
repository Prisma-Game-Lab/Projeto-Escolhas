//using System;
//using System.Text;
//using System.IO;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

/*
 * 
 * Script responsible for the read/write to the persistent info, saved in a json file
 * Author: André Mazal Krauss
 * 
 */

 /*
public class SaveSystem : MonoBehaviour
{
    [HideInInspector] //deve ficar escondido, só tiro enquanto programo, se eu esquecer podem por de volta pfv
    public PointSystem pointSystem;
    public PointSystem emptySave;

    //a versão corrente do save, para podermos testar e tratar versões antigas
    public static float saveVersion = 1.0f;
    
    private static string saveFileName = "save";
    private static string SavePath
    { 
        get
        {
            return Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
        }
    }

    private static string versionSaveName = "version";
    private static string VersionSavePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, versionSaveName + ".ver");
        }
    }

    //usado para, a todo momento, saber o último level que foi jogado, ou está sendo jogado
    public int currLevelPage;
    public int currLevelNumber;

    public string currLevelString;

    //usado quando o countdown de um reset, quando ele não deve descarregar a cena
    public bool CountdownDontReloadScene;

    private static SaveSystem instance;
    public static SaveSystem GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if(instance != null)
        {
            //não pode haver 2 desse script em cena
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            //tenta loadar
            if(!LoadState())
            {
                //se falhou, instancia novo save e o salva
                pointSystem = GameObject.Instantiate(emptySave);
                SaveState();
                string path = Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
                Debug.Log("new save on path:" + path);

                currLevelPage = 0;
                currLevelNumber = 0;
            }        
        }
        
        DontDestroyOnLoad(this.gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SaveState()
    {
        //string path = Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
        string json_ps = JsonUtility.ToJson(pointSystem);
        byte[] buffer = Encoding.UTF8.GetBytes(json_ps);
        for(int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = (Byte)((~buffer[i]) & 0xFF); //faço um simples not. Não é seguro, mas é o suficiente pra ninguém editar na mão o arquivo 
        }
        try
        {
            File.WriteAllBytes(SavePath, buffer);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Unable to write to savefile");
        }
        
        //registra versão atual do save
        using (StreamWriter streamWriter = File.CreateText (VersionSavePath))
        {
            streamWriter.Write (saveVersion.ToString());
        }

    }

    private bool ValidateOldSave()
    {
        Debug.LogWarning("Updating old save files was not yet implemented");
        return false;
    }

    //retorna true se conseguiu ler, senão false
    public bool LoadState()
    {
        string path = SavePath;
        string versionPath = VersionSavePath;
        //checar se existe save
        if(!File.Exists(path) || !File.Exists(versionPath))
		{
			return false;
		}
        
        //verifica versão do save        
        using (StreamReader streamReader = File.OpenText (versionPath))
        {
            string str = streamReader.ReadToEnd ();
            try
            {
                float ver = float.Parse(str);
                if (ver < saveVersion)
                {
                    //versão do save desatualizada
                    Debug.LogWarning("Warning: old save version");
                    return ValidateOldSave();
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Warning: invalid save-version format" + e.Message);
                return ValidateOldSave();
            }
        }
       
        try
        {
            byte[] buffer = File.ReadAllBytes(path);
            for(int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (Byte)((~buffer[i]) & 0xFF); //faço um simples not. Não é seguro, mas é o suficiente pra ninguém editar na mão o arquivo 
            }
            string jsonString = Encoding.UTF8.GetString(buffer);
            pointSystem = ScriptableObject.CreateInstance<PointSystem>();
            JsonUtility.FromJsonOverwrite(jsonString, pointSystem);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Unable to load savefile " + e.Message);
            return false;
        }

    }

    //função helper pra setar score do level corrente. Só pra evitar gets/sets bobos
    public void SetScoreForCurrentLevel(int stars, bool cleared, bool[] charsSaved)
    {
        pointSystem.UpdateLevel(currLevelPage, currLevelNumber, stars, cleared, charsSaved);
        SaveState();
    }

    public void ClearState()
    {
        pointSystem = GameObject.Instantiate(emptySave); 
    }

    //[MenuItem("OurGame/CleanSave")] por algum motivo doido isso quebra a build?
    public static void DeleteSaveFile()
    {
       try
       {
            File.Delete(SavePath);
            File.Delete(VersionSavePath);
            if(instance != null)
            {
                instance.pointSystem = GameObject.Instantiate(instance.emptySave);
                
                //seto a página e level correntes para a primeira
                instance.currLevelPage = 0;
                instance.currLevelNumber = 0;

            }
       }
       catch(Exception e)
       {
           Debug.LogWarning("Could not delete file:" + e.Message);
       }
    }

    //ao sair do jogo, salvar estado
    void OnApplicationQuit()
    {
        SaveState();
    }

}
*/