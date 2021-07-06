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
    
    public void AddCommand(ICommand command)
    {
        _commandBuffer.Add(command);
        _commandIndex = _commandBuffer.Count-1;
    }

    public void StepThroughCommands(bool back)
    {
        if (back)
        {
            _commandBuffer[_commandIndex].Undo();
            _commandIndex--;
            if (_commandIndex < 0)
            {
                _commandIndex = 0;
            }
        }

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
