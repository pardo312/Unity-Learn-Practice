public class ScaleCommand : Command {
    private readonly float scaleFactor;
    public ScaleCommand(ICharacter character, float scaleDirection) : base(character) {
        this.scaleFactor = scaleDirection == 1f ? 1.1f : 0.9f;
    }

    public override void Execute() {
        character.transform.localScale *= scaleFactor;
    }

    public override void Undo() {
        character.transform.localScale /= scaleFactor;
    }
}