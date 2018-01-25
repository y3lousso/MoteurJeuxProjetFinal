# 2D Game Engine

A simple 2D component-entity game engine to create script-based games.
This game engine is develloped in C#, such as the scripts.

With this engine, you can create and run small 2D games. It handles sprite rendering, audio, input personalisation and includes a very simple scripting system. You can also personalize physics and collisions.

This engine is compatible with our ([editor](https://github.com/y3lousso/GameEngineEditor)) to create all the scenes of your game. The editor exports your game as an xml file. You also have to write scripts in C# (see below to have instructions).


## Authors

* **Yannick Lounssou**
* **Thomas Faget**
* **Malek El Ouerghi**
* **Félix Lecate**

## Getting Started

### Import and run the game engine

To be able to use the game engine, download and DLL and add it in your project with 

```
using Engine;
```

To create an instance of the game engine, the step will be

```
GameEngine engine = new GameEngine();
```

Next set path of the assets with

```
gameEngine.imagePath = "Path of sprites";
gameEngine.audioPath = "Path of audio";
gameEngine.inputsPath = "Path of inputs";
gameEngine.InitForXml("Path of xml data");
```

And finally run the game engine

```
gameEngine.RunGameLoop()
```

### Adding scripts

To create a script, you just have to implement the class `GameScript`

```
using Engine;

class Player1Script : GameScript
{

    public override void Start(ActionManager actionManager)
    {
    // Process on start
    }

    public override void Update()
    {
    // Process on each frame
    }
}
```

You can catch the engine events with your script. Just override a listener method in your script

```
        // The listener methods
        public virtual void OnGameStart(GameStartEvent gameStartEvent) {}
        public virtual void OnGameFinish(GameFinishEvent gameFinishEvent) {}
        public virtual void OnCollision(CollisionEvent collisionEvent) {}
        public virtual void OnSceneChange(SceneChangeEvent sceneChangeEvent) {}
        public virtual void OnNewSceneDisplayed(NewSceneDisplayedEvent sceneChangeEvent) {}
        public virtual void OnClick(EntityClickEvent entityClickEvent) {}
```

To associate the script with an entity, use the Game Editor.
The script file need to be your game project folder !

### Create assets

The game engine handles sprites and audio files. To add those files in your game, set the `audioPath` and `imagePath` of the game engine.

With the game engine, you can create all the scenes of your game. This editor exports your game scenes in a xml file.

You can find a example of a game create with this engine for more help.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Build with

The engine is wrote in C#. The rendering use the .NET library.









