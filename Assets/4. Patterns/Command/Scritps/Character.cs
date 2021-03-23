using UnityEngine;

public class Character : MonoBehaviour, ICharacter {

    private InputReader inputReader;
    private CommandProcessor commandProcessor;
    void Awake() {
        inputReader = GetComponent<InputReader>();
        commandProcessor = GetComponent<CommandProcessor>();
    }

    void Update() {
        var direction = inputReader.ReadInput();

        if (direction != Vector3.zero) {
            var moveCommand = new MoveCommand(this, direction);
            commandProcessor.ExecuteCommand(moveCommand);
        }

        if (inputReader.ReadUndo()) {
            commandProcessor.Undo();
        }

        float scaleDirection = inputReader.ReadScale();

        if(scaleDirection!= 0f)
            commandProcessor.ExecuteCommand(new ScaleCommand(this,scaleDirection));
    }

    public void MoveFromTo(Vector3 startPosition, Vector3 endPosition) {
        throw new System.NotImplementedException();
    }
}
