namespace Game
{
    public interface IEnemyState
    {
        public EnemySM SM { get; set; }

        void Enter();
        void Tick();
        void Exit();
    }
}