using UnityEngine;

public class MoveCommand : Command {

    private Vector3 direction;

    public MoveCommand(ICharacter character, Vector3 direction) : base(character) {
        this.direction = direction;
    }

    public override void Execute() {
        character.transform.position += direction * 0.1f;
    }

    public override void Undo() {
        character.transform.position -= direction * 0.1f;
    }
}
