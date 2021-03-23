public abstract class Command {

    protected ICharacter character;

    public Command(ICharacter character) {
        this.character = character;
    }

    public abstract void Execute();
    public abstract void Undo();
}