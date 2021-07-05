using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCommand : ICommand
{
    GameObject _panel;

    public PanelCommand(GameObject panel)
    {
        this._panel = panel;
    }

    public void Execute()
    {
        //enable the next panel
        _panel.SetActive(true);
        CommandManager.Instance.AddCommand(this);
    }

    public void Undo()
    {
        //disable most recent panel
        _panel.SetActive(false);
    }

    public void Redo()
    {
        //re enable most recent panel
        _panel.SetActive(true);
    }
}
