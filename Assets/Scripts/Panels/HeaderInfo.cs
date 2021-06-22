using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderInfo : MonoBehaviour, IPanel
{
    public Text caseNumberText;

    public void OnEnable()
    {
        caseNumberText.text = UIManager.Instance.activeCase.caseID;
    }

    public void ProcessInfo()
    {
        throw new System.NotImplementedException();
    }
}
