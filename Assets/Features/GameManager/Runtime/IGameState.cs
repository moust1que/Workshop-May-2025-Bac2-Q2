namespace GameManager.Runtime {
    public interface IGameState {
        void Enter(GameManager game);
        void Exit(GameManager game);
        void Update(GameManager game);
    }
}