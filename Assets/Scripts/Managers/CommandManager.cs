using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private static CommandManager _instance;
    public static CommandManager Instance
    {
        get
        {
            if (_instance == null)
                throw new UnityException("The Command Manager is Null!");
            return _instance;
        }
    }

    private List<ICommand> _commandBuffer = new List<ICommand>();
    private int _commandIndex;
    
    //create method to add commands to the command buffer
    public void AddCommand(ICommand command)
    {
        //add command to list
        _commandBuffer.Add(command);
        //set _commandIndex to list length
        _commandIndex = _commandBuffer.Count-1;
    }

    //create a method to step +1 or -1 through list
    public void StepThroughCommands(bool back)
    {
        //if back
        //  commandBuffer[listIndex].undo()
        //  listIndex--

        if (back)
        {
            _commandBuffer[_commandIndex].Undo();
            _commandIndex--;
            if (_commandIndex < 0)
            {
                _commandIndex = 0;
            }
        }

        //else
        //  commandBuffer[listIndex].redo()
        //  listIndex++
        //  if listIndex > list length
        //  listIndex = list length

        else
        {
            _commandBuffer[_commandIndex].Redo();
            _commandIndex++;
            if (_commandIndex >= _commandBuffer.Count)
                _commandIndex = _commandBuffer.Count-1;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
}
