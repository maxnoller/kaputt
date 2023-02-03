namespace NOBRAIN.CrackStrike.GameState{
abstract class GameState{
	public abstract void Enter();
	public abstract void Exit();
	public virtual void StartGame(){}
}
}