using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCasePanel : MonoBehaviour, IPanel
{
    public Text informationText;
    public GameObject overviewPanel;

    //onenable
    //populate data with case data


    private void OnEnable()
    {
        informationText.text = "Case ID: " + UIManager.Instance.activeCase.caseID + "\n" 
                               + UIManager.Instance.activeCase.clientName + "\n" 
                               + UIManager.Instance.activeCase.date + "\n";
    }

    public void ProcessInfo()
    {
        ICommand panelCommand = new PanelCommand(overviewPanel);
        panelCommand.Execute();
    }

}