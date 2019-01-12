using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld.Models
{
    public class Animations
    {
        public int CurrentFrame { get; set; }

        public int FrameCount { get; private set; }

        public int FrameHeight { get { return Texture.Height; } }

        public float FrameSpeed { get; set; }

        public int FrameWidth { get { return Texture.Width / FrameCount; } }

       // public bool IsLooping { get; set; }

        public bool FlipImage { get; private set; }

        public bool isAttacking { get; private set; }

        public Texture2D Texture { get; private set; }

        public Animations(Texture2D texture, int frameCount, bool flipImage, bool isAttack)
        {
            Texture = texture;

            FlipImage = flipImage;

            isAttacking = isAttack;

            Constants.TEXTURE = texture;

            FrameCount = frameCount;

           // IsLooping = true;

            FrameSpeed = 0.1f;
        }
    }
}
