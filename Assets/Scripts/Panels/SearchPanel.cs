using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchPanel : MonoBehaviour, IPanel
{
    public InputField caseNumberInput;
    public void ProcessInfo()
    {
        //dl list of all objects inside S3 storage

        AWSManager.Instance.GetList(caseNumberInput.text);
        
        //compare those to casenumber input by user
        
        
        //if we find a match
        //download that object

    }
}
