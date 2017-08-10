using UnityEngine;
using System.Collections;

//Librarias para almacenamiento interno
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

    public static GameControl instance;
    public datoUsuario objUsuario;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            objUsuario = loadFromDevice();
        }
    }

    public void saveOnDevice(datoUsuario data)
    {
        //We create the binary variable
        BinaryFormatter bf = new BinaryFormatter();
        //Path and name of the file we are going to save (Create File)
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        //Serialize the data class
        bf.Serialize(file, data);
        //Close the file
        file.Close();
    }

    public datoUsuario loadFromDevice()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //Get the file from the path
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
        //Deserialize the data so we can read it
        datoUsuario data = (datoUsuario)bf.Deserialize(file);
        file.Close();
        //Return the data retrieved
        return data;
    }
}
