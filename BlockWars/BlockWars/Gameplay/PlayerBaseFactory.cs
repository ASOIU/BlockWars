using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2D.XNA;

namespace BlockWars.Gameplay
{
    static class PlayerBaseFactory
    {
        private const int BASE_STRENGTH = 1000;

        public static List<Box> CreateBuilding(World world, Gameplay gameplay, EntityCategory playerType)
        {
            List<Box> boxes = new List<Box>();
            Terrain terrain = new Terrain(world);
            bool flag = true;

            switch (playerType)
            {
                case (EntityCategory.Player1):
                    {
                        float x, y;
                        y = terrain.GetHeight(-300);
                        float bw, bh;
                        bw = 6;
                        bh = 3;
                        Vector2 position;
                        Vector2 size;
                        Box box;
                        for (int i = 0; i < 8; i++)
                        {
                            x = -300;
                            for (int j = 0; j < 3; j++)
                            {
                                if (j >= 1 && i>=1)
                                {
                                    flag = false;
                                }
                                CreateNew(flag, terrain, x, y, bw, bh, boxes, world, gameplay);
                                position = new Vector2(x, y);
                                size = new Vector2(bw, bh);
                                box = new Box(world, position, size, "base-block", true, gameplay.Player1, BASE_STRENGTH);
                                boxes.Add(box);
                                x += bw;
                            }
                            y += bh;
                        }
                        flag = true;
                        x = -303;
                        y = terrain.GetHeight(x) + 24f;
                        for (int i = 0; i < 4; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "base-block", true, gameplay.Player1, BASE_STRENGTH);
                            boxes.Add(box);
                            x += bw;
                        }
                        x = -303;
                        y = terrain.GetHeight(x) + 27f;
                        for (int i = 0; i < 3; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "base-block", true, gameplay.Player1, BASE_STRENGTH);
                            boxes.Add(box);
                            x += bw + bw / 2f;
                        }
                        y = terrain.GetHeight(-230);
                        for (int i = 0; i < 8; i++)
                        {
                            x = -230;
                            for (int j = 0; j < 3; j++)
                            {
                                if (j >= 1 && i >= 1)
                                {
                                    flag = false;
                                }
                                CreateNew(flag, terrain, x, y, bw, bh, boxes, world, gameplay);
                                position = new Vector2(x, y);
                                size = new Vector2(bw, bh);
                                box = new Box(world, position, size, "block2", true, gameplay.Player1,200);
                                boxes.Add(box);
                                x += bw;
                            }
                            y += bh;
                        }
                        x = -233;
                        y = terrain.GetHeight(-233)+24f;
                        for (int i = 0; i < 4; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "block2", true, gameplay.Player1,200);
                            boxes.Add(box);
                            x += bw;
                        }
                        x = -233;
                        y = terrain.GetHeight(-233)+27f;
                        for (int i = 0; i < 3; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "block2", true, gameplay.Player1,200);
                            boxes.Add(box);
                            x += bw + bw / 2f;
                        }
                        flag = true;
                        y = terrain.GetHeight(-236);
                        for (int i = 0; i < 5; i++)
                        {
                            x = -236;
                            for (int j = 0; j < 8; j++)
                            {
                                if (j >= 1 && i >= 1)
                                {
                                    flag = false;
                                }
                                CreateNew(flag, terrain, x, y, bw, bh, boxes, world, gameplay);
                                position = new Vector2(x, y);
                                size = new Vector2(bw, bh);
                                box = new Box(world, position, size, "block3", true, gameplay.Player1);
                                boxes.Add(box);
                                x -= bw;
                            }
                            y += bh;
                        }
                        flag = true;
                        break;
                    }
                case (EntityCategory.Player2):
                    {
                        float x, y;
                        y = terrain.GetHeight(300);
                        float bw, bh;
                        bw = 6;
                        bh = 3;
                        Vector2 position;
                        Vector2 size;
                        Box box;
                        for (int i = 0; i < 8; i++)
                        {
                            x = 300;
                            for (int j = 0; j < 3; j++)
                            {
                                position = new Vector2(x, y);
                                size = new Vector2(bw, bh);
                                box = new Box(world, position, size, "base-block", true, gameplay.Player2, BASE_STRENGTH);
                                boxes.Add(box);
                                x -= bw;
                            }
                            y += bh;
                        }
                        x = 303;
                        y = terrain.GetHeight(300)+24f;
                        for (int i = 0; i < 4; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "base-block", true, gameplay.Player2, BASE_STRENGTH);
                            boxes.Add(box);
                            x -= bw;
                        }
                        x = 303;
                        y = terrain.GetHeight(300)+27f;
                        for (int i = 0; i < 3; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "base-block", true, gameplay.Player2, BASE_STRENGTH);
                            boxes.Add(box);
                            x -= bw + bw / 2f;
                        }
                        y = terrain.GetHeight(230);
                        for (int i = 0; i < 8; i++)
                        {
                            x = 230;
                            for (int j = 0; j < 3; j++)
                            {
                                position = new Vector2(x, y);
                                size = new Vector2(bw, bh);
                                box = new Box(world, position, size, "block2", true, gameplay.Player2,200);
                                boxes.Add(box);
                                x -= bw;
                            }
                            y += bh;
                        }
                        x = 233;
                        y = terrain.GetHeight(233)+24f;
                        for (int i = 0; i < 4; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "block2", true, gameplay.Player2,200);
                            boxes.Add(box);
                            x -= bw;
                        }
                        x = 233;
                        y = terrain.GetHeight(233)+27f;
                        for (int i = 0; i < 3; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "block2", true, gameplay.Player2,200);
                            boxes.Add(box);
                            x -= bw + bw / 2f;
                        }
                        y = terrain.GetHeight(236);
                        for (int i = 0; i < 5; i++)
                        {
                            x = 236;
                            for (int j = 0; j < 8; j++)
                            {
                                position = new Vector2(x, y);
                                size = new Vector2(bw, bh);
                                box = new Box(world, position, size, "block3", true, gameplay.Player2);
                                boxes.Add(box);
                                x += bw;
                            }
                            y += bh;
                        }
                        break;
                    }
            }
            return boxes;
        }

        private static void CreateNew(bool flag, Terrain terrain, float x, float y, float bw, float bh, List<Box> boxes, World world, Gameplay gameplay)
        {
            Vector2 position;
            Vector2 size;
            Box box;
            if (flag)
            {
                float checkY;
                int number;
                checkY = terrain.GetHeight(x);
                if (checkY != y)
                {
                    checkY = y - checkY;
                    number = (int)(checkY / 3);
                    Console.WriteLine(number);
                    float newY;
                    newY = checkY;
                    for (int k = 1; k <= number; k++)
                    {
                        newY += bh;
                        Console.WriteLine(newY);
                        position = new Vector2(x, newY);
                        size = new Vector2(bw, bh);
                        box = new Box(world, position, size, "base-block", true, gameplay.Player1, BASE_STRENGTH);
                        boxes.Add(box);
                        Console.WriteLine("Yes!");
                    }
                }
            }
        }
    }
}
