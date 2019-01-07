using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorld
{
    class Map
    {
        public Texture2D texture;
        public bool isDeadly;
       // private Tiles[,] blokArray = new Tiles[8, 18];
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();
        
      
        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }

        }

        private int width, height;
        public int Width
        {
            get { return width; }
        }
         
        public int Height
        {
            get { return height; }
        }

        public Map() { }

        public void Generate(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0 && number < 3)
                    {
                        collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size), isDeadly = false));
                    }
                    if (number == 3)
                    {
                        collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size), isDeadly = true));
                    }
                    if (number == 4)
                    {
                        collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size+20, y * size+39,25,25), isDeadly = false));
                    }





                    width = (x + 1) * size;

                    height = (y + 1) * size;

                }
            }


        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionTiles tile  in collisionTiles)
            {
                tile.Draw(spriteBatch);
            }
        }

        public void Level1()
        {
            Generate(new int[,]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,1,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,1,2,1,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,1,2,2,0,1,0,0,0,0,0,0,0},
                { 0,0,1,0,0,1,2,2,2,0,0,0,0,0,0,0,1,0},
                { 1,2,2,3,3,2,2,2,2,2,2,2,3,2,1,3,2,1},
            }, 64);
        }
        public void Level2()
        {
            Generate(new int[,]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,1,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,1,2,1,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,1,2,2,0,1,0,0,0,0,0,0,0},
                { 0,0,1,0,0,1,2,2,2,0,0,0,0,0,0,0,1,0},
                { 1,2,2,3,3,2,2,2,2,2,2,2,3,2,1,3,2,1},
            }, 64);
        }
    }
}
