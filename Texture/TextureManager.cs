using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
        public enum TextureName
    {
        Ball,
        Paddle,
        GameFont,
        TitleScreen,
        Heart,
        RedBlock,
        GoldBlock,
        BlueBlock,
        GreenBlock,
        PlayButton,
        OptionsButton,
    }
    
    public class TextureManager
    {
        private Dictionary<TextureName, Texture2D> textures = new Dictionary<TextureName, Texture2D>();

        public void Add(TextureName name, Texture2D texture)
        {
            textures.Add(name, texture);
        }

        public Texture2D Get(TextureName name)
        {
            return textures[name];
        }
    }
}
