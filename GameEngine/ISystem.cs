namespace MoteurJeuxProjetFinal.GameEngine
{
    interface ISystem
    {
        void Start(GameEngine gameEngine);

        void Update(float deltaTime);

        void End();

        void AddEntity(Entity entity);

        void RemoveEntity(Entity entity);
    }
}
