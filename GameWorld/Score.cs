using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld
{
    class Score
    {
        public SpriteFont ScoreFont { get; }
        public Vector2 Position { get; set; } = new Vector2(0, 0);
        public static int ScorePoints { get; set; } = 0;


        public Score(SpriteFont scoreFont)
        {
            ScoreFont = scoreFont;
        }

        public void Draw(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
        {

            spriteBatch.DrawString(ScoreFont, "Coins: " + ScorePoints, Position, Color.White);
        }
    }
}
