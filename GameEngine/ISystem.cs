using System.Collections.Generic;

namespace MoteurJeuxProjetFinal.GameEngine
{
    interface ISystem
    {
        void Start(GameEngine gameEngine);

        void Update(float deltaTime);

        void End();

        /// <summary>
        /// Indicate if an entity can be compatible with this systems
        /// I.e. It has the requiered Components
        /// </summary>
        bool IsCompatible(Entity entity);

        /// <summary>
        /// Add an entity and create an node with the entity
        /// The entity is added only if it is compatible with the system
        /// </summary>
        void AddEntity(Entity entity);

        /// <summary>
        /// Edit an entity in the system, such as :
        ///  - If the new Entity is compatible, but not the old => Add the new Entity (AddEntity)
        ///  - If the old Entity is compatible, but not the new => Remove the old entity (RemoveEntity)
        ///  - If both are compatible => Replace the old Entity by the new
        ///  - If both are not compatible => Do nothing
        /// </summary>
        void EditEntity(Entity oldEntity, Entity newEntity);

        /// <summary>
        /// Remove the node associated with the entity
        /// </summary>
        void RemoveEntity(Entity entity);

        /// <summary>
        /// Init the nodes of the system with a list of entity
        /// </summary>
        void InitEntities(List<Entity> entities);
    }
}
