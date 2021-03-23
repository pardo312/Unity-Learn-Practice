using UnityEngine;
using System.Collections.Generic;
public class CommandProcessor : MonoBehaviour {

    private List<Command> commands = new List<Command>();

    private int currentCommandIndex;

    public void ExecuteCommand(Command command) {
        commands.Add(command);
        command.Execute();
        currentCommandIndex = commands.Count - 1;
    }

    public void Undo() {
        if (currentCommandIndex < 0)
            return;

        commands[currentCommandIndex].Undo();
        commands.RemoveAt(currentCommandIndex);
        currentCommandIndex--;
    }

    public void Redo() {
        commands[currentCommandIndex].Execute();
        currentCommandIndex++;
    }
}
