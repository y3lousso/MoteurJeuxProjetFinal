namespace MoteurJeuxProjetFinal.GameEngine
{
    interface ISystem
    {
        void Start(GameEngine gameEngine);

        void Update(float deltaTime);

        void End();
    }
}
