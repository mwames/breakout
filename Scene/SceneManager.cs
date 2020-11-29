using System;
using System.Collections.Generic;

namespace Breakout
{
    public enum SceneName
    {
        Menu,
        Game,
        GameOver,
        Pause,
        Editor,
        Death,
        Options
    }

    public class SceneManager
    {
        public SceneName sceneName;
        public IScene Scene => scenes[sceneName];
        public Dictionary<SceneName, IScene> scenes = new Dictionary<SceneName, IScene>();

        public void Add(SceneName name, IScene scene)
        {
            scenes.Add(name, scene);
        }

        public void ChangeScene(SceneName name)
        {
            this.sceneName = name;
        }
    }
}
