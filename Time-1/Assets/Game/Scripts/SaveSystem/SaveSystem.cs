using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
 
public class SaveSystem : MonoBehaviour
{
    [HideInInspector] //deve ficar escondido, só tiro enquanto programo, se eu esquecer podem por de volta pfv
    public AppSave appSave;
    public AppSave emptySave;
    private TinderData tinderData;
    private playerStats playerStats;

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
            //tenta dar load
            if(!LoadState())
            {
                //se falhou, instancia novo save e o salva
                appSave = GameObject.Instantiate(emptySave);
                SaveState();
                appSave.elfaJson = "";
                appSave.orcJson = "";
                appSave.sereiaJson = "";
                appSave.humanoJson = "";
                appSave.elfaEndDay = false;
                appSave.orcEndDay = false;
                appSave.sereiaEndDay = false;
                appSave.humanoEndDay = false;
                appSave.askTutorialOn = true;
                appSave.elfaPoints = 0;
                appSave.humanoPoints = 0;
                appSave.sereiaPoints = 0;
                appSave.orcPoints = 0;
                appSave.love = 0;
                appSave.elfaBattle = false;
                appSave.orcBattle = false;
                appSave.sereiaBattle = false;
                appSave.humanoBattle = false;
                appSave.matchesNumber = 0;
                appSave.blockedCharacters.Clear();
                appSave.renewDay = false;
                string path = Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
                Debug.Log("new save on path:" + path);

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
        string json_ps = JsonUtility.ToJson(appSave);
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
            Debug.LogWarning("Unable to write to savefile" + e.Message);
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
            appSave = ScriptableObject.CreateInstance<AppSave>();
            JsonUtility.FromJsonOverwrite(jsonString, appSave);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Unable to load savefile " + e.Message);
            return false;
        }

    }

    public static void DeleteSaveFile()
    {
       try
       {
            File.Delete(SavePath);
            File.Delete(VersionSavePath);
            if(instance != null)
            {
                instance.appSave = GameObject.Instantiate(instance.emptySave);
            }
       }
       catch(Exception e)
       {
           Debug.LogWarning("Could not delete file:" + e.Message);
       }
    }

    public void NewGame() 
    {
        tinderData = GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>();
        playerStats = GameObject.FindGameObjectWithTag("persistentData").GetComponent<playerStats>();

        appSave.elfa.Clear();
        appSave.humano.Clear();
        appSave.sereia.Clear();
        appSave.orc.Clear();

        appSave.elfaJson = "";
        appSave.orcJson = "";
        appSave.sereiaJson = "";
        appSave.humanoJson = "";

        appSave.elfaEndDay = false;
        appSave.orcEndDay = false;
        appSave.sereiaEndDay = false;
        appSave.humanoEndDay = false;

        appSave.askTutorialOn = true;

        appSave.elfaPoints = 0;
        appSave.humanoPoints = 0;
        appSave.sereiaPoints = 0;
        appSave.orcPoints = 0;

        tinderData.elfaDay = 0;
        tinderData.humanoDay = 0;
        tinderData.orcDay = 0;
        tinderData.sereiaDay = 0;
        tinderData.curDay = 1;

        tinderData.tinderCharacters.Clear();
        tinderData.curContacts.Clear();

        appSave.blockedCharacters.Clear();

        tinderData.tinderCharacters.Add(Resources.Load<CharacterBase>("Characters/Fakes/Carol"));
        tinderData.tinderCharacters.Add(Resources.Load<CharacterBase>("Characters/Elfa"));
        tinderData.tinderCharacters.Add(Resources.Load<CharacterBase>("Characters/Fakes/Madu"));
        tinderData.tinderCharacters.Add(Resources.Load<CharacterBase>("Characters/Humano"));
        tinderData.tinderCharacters.Add(Resources.Load<CharacterBase>("Characters/Fakes/Mark"));
        tinderData.tinderCharacters.Add(Resources.Load<CharacterBase>("Characters/Sereia"));
        tinderData.tinderCharacters.Add(Resources.Load<CharacterBase>("Characters/Fakes/Ozório"));
        tinderData.tinderCharacters.Add(Resources.Load<CharacterBase>("Characters/Orc"));

        CharacterBase player = Resources.Load<CharacterBase>("Characters/Player");

        appSave.elfaBattle = false;
        appSave.orcBattle = false;
        appSave.sereiaBattle = false;
        appSave.humanoBattle = false;

        appSave.renewDay = false;

        playerStats.attack = player.attack;
        playerStats.defense = player.defense;
        playerStats.velocity = player.velocity;
        playerStats.maxEnergy = player.maxEnergy;
        //playerStats.maxHealth = player.maxHealth;
        playerStats.availableStatsPoints = 100;

        SceneManager.LoadScene("MainMenu_Scene");

    }

}
