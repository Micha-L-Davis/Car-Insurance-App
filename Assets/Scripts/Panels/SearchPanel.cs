using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchPanel : MonoBehaviour, IPanel
{
    public InputField caseNumberInput;
    public SelectCasePanel selectCasePanel;
    public void ProcessInfo()
    {
        //dl list of all objects inside S3 storage

        AWSManager.Instance.GetList(caseNumberInput.text, () => 
        {
            selectCasePanel.gameObject.SetActive(true);
        });
        
        //compare those to casenumber input by user
        
        
        //if we find a match
        //download that object

    }
}
