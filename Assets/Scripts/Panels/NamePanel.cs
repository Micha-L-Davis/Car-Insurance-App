using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePanel : MonoBehaviour, IPanel
{
    public InputField firstName, lastName;
    public GameObject locationPanel;
    public void ProcessInfo()
    {
        if (string.IsNullOrEmpty(firstName.text) || string.IsNullOrEmpty(lastName.text))
        {
            throw new UnityException("Either the first or last name is NULL, cannot continue");
        }
        else
        {
            UIManager.Instance.activeCase.clientName = "" + firstName.text + " " + lastName.text + "";
            locationPanel.SetActive(true);
        }
    }
}