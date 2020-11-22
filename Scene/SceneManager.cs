using System;
using System.Collections.Generic;

namespace Breakout
{
    public class SceneManager
    {
        public IScene currentScene;
        private Dictionary<SceneName, IScene> scenes = new Dictionary<SceneName, IScene>();

        public void Add(SceneName name, IScene scene) {
            Console.WriteLine(name.ToString(), (scene == null).ToString());
            scenes.Add(name, scene);
        }

        public IScene Get(SceneName name) {
            return scenes[name];
        }
    }
}
