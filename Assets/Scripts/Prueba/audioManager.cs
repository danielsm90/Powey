using UnityEngine;
using System.Collections;

//Librarias para almacenamiento interno
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class audioManager : MonoBehaviour {

    public static audioManager instance;

    public audioInfo objAudio;

    public GameObject audioBotones;

    private bool bandBotones, bandEscenas;

	// Use this for initialization
	void Awake () {
        instance = this;
        objAudio = new audioInfo();
        if (File.Exists(Application.persistentDataPath + "/audioInfo.dat"))
        {
            objAudio = loadConfAudio();
        }
        else
        {
            saveConfAudio(objAudio);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void saveConfAudio(audioInfo data)
    {
        //We create the binary variable
        BinaryFormatter bf = new BinaryFormatter();
        //Path and name of the file we are going to save (Create File)
        FileStream file = File.Create(Application.persistentDataPath + "/audioInfo.dat");
        //Serialize the data class
        bf.Serialize(file, data);
        //Close the file
        file.Close();
    }

    public audioInfo loadConfAudio()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //Get the file from the path
        FileStream file = File.Open(Application.persistentDataPath + "/audioInfo.dat", FileMode.Open);
        //Deserialize the data so we can read it
        audioInfo data = (audioInfo)bf.Deserialize(file);
        file.Close();
        //Return the data retrieved
        return data;
    }
}
