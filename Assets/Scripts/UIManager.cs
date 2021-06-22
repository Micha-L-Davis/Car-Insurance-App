using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private List<Case> cases = new List<Case>();

    public void CreateNewCase()
    {
        activeCase = new Case();
        activeCase.caseID = "CASE NUMBER " + cases.Count;
        cases.Add(activeCase);
        //generate a caseID
        //between 000 and 999
        //update active caseID


    }



    private void Awake()
    {
        _instance = this;
    }

}
