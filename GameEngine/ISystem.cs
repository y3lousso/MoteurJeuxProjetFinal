namespace MoteurJeuxProjetFinal.GameEngine
{
    interface ISystem
    {
        void Start(GameEngine _gameEngine);

        void Update(float deltaTime);

        void End();
    }
}
