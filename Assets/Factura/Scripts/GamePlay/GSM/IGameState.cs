namespace Game
{
    public interface IGameState
    {
        public GameSM GameSM { get; set; }
        
        void Enter();
        void Exit();
    }
}
