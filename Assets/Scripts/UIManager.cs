using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new UnityException("The UI Manager is NULL");
            }
            return _instance;
        }
    }

    public Case activeCase;
    //private List<Case> cases = new List<Case>();

    public void CreateNewCase()
    {
        activeCase = new Case();
        int id = UnityEngine.Random.Range(0, 1000);
        activeCase.caseID = "" + id;
        activeCase.date = DateTime.Today.ToString();
        //cases.Add(activeCase);
        //generate a caseID
        //between 000 and 999
        //update active caseID


    }

    public void SubmitButton()
    {
        //create a case to save
        //populate case data
        //open a data stream to turn object into a file
        //begin AWS process

        Case aWSCase = new Case();
        aWSCase.caseID = activeCase.caseID;
        aWSCase.clientName = activeCase.clientName;
        aWSCase.date = activeCase.date;
        aWSCase.locationNotes = activeCase.locationNotes;
        aWSCase.photoData = activeCase.photoData;
        aWSCase.photoNotes = activeCase.photoNotes;

        BinaryFormatter bf = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/" + aWSCase.caseID + ".dat";
        FileStream file = File.Create(filePath);
        bf.Serialize(file, aWSCase);
        file.Close();
        Debug.Log("Application Data Path: " + Application.persistentDataPath);

        //Send to AWS
        AWSManager.Instance.UploadToS3(filePath, aWSCase.caseID);

    }


    private void Awake()
    {
        _instance = this;
    }

}
