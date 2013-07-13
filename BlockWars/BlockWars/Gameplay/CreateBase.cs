﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2D.XNA;

namespace BlockWars.Gameplay
{
    class CreateBase
    {
        private List<Box> mBoxes;
        const int strength = 1000;

        public CreateBase()
        {
            mBoxes = new List<Box>();
        }

        public List<Box> CreateBuilding(World world, Gameplay gameplay, EntityCategory playerType)
        {
            switch (playerType)
            {
                case (EntityCategory.Player1):
                    {
                        float x, y;
                        y = -18f;
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
                                position = new Vector2(x, y);
                                size = new Vector2(bw, bh);
                                box = new Box(world, position, size, "base-block", true, gameplay.Player1, strength);
                                mBoxes.Add(box);
                                x += bw;
                            }
                            y += bh;
                        }
                        x = -303;
                        y = 5.8f;
                        for (int i = 0; i < 4; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "base-block", true, gameplay.Player1, strength);
                            mBoxes.Add(box);
                            x += bw;
                        }
                        x = -303;
                        y = 8.8f;
                        for (int i = 0; i < 3; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "base-block", true, gameplay.Player1, strength);
                            mBoxes.Add(box);
                            x += bw + bw / 2f;
                        }
                        y = -18f;
                        for (int i = 0; i < 8; i++)
                        {
                            x = -230;
                            for (int j = 0; j < 3; j++)
                            {
                                position = new Vector2(x, y);
                                size = new Vector2(bw, bh);
                                box = new Box(world, position, size, "block2", true, gameplay.Player1,200);
                                mBoxes.Add(box);
                                x += bw;
                            }
                            y += bh;
                        }
                        x = -233;
                        y = 5.8f;
                        for (int i = 0; i < 4; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "block2", true, gameplay.Player1,200);
                            mBoxes.Add(box);
                            x += bw;
                        }
                        x = -233;
                        y = 8.8f;
                        for (int i = 0; i < 3; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "block2", true, gameplay.Player1,200);
                            mBoxes.Add(box);
                            x += bw + bw / 2f;
                        }
                        y = -18f;
                        for (int i = 0; i < 5; i++)
                        {
                            x = -236;
                            for (int j = 0; j < 8; j++)
                            {
                                position = new Vector2(x, y);
                                size = new Vector2(bw, bh);
                                box = new Box(world, position, size, "block3", true, gameplay.Player1);
                                mBoxes.Add(box);
                                x -= bw;
                            }
                            y += bh;
                        } 
                        break;
                    }
                case (EntityCategory.Player2):
                    {
                        float x, y;
                        y = -18f;
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
                                box = new Box(world, position, size, "base-block", true, gameplay.Player2, strength);
                                mBoxes.Add(box);
                                x -= bw;
                            }
                            y += bh;
                        }
                        x = 303;
                        y = 5.8f;
                        for (int i = 0; i < 4; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "base-block", true, gameplay.Player2, strength);
                            mBoxes.Add(box);
                            x -= bw;
                        }
                        x = 303;
                        y = 8.8f;
                        for (int i = 0; i < 3; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "base-block", true, gameplay.Player2, strength);
                            mBoxes.Add(box);
                            x -= bw + bw / 2f;
                        }
                        y = -18f;
                        for (int i = 0; i < 8; i++)
                        {
                            x = 230;
                            for (int j = 0; j < 3; j++)
                            {
                                position = new Vector2(x, y);
                                size = new Vector2(bw, bh);
                                box = new Box(world, position, size, "block2", true, gameplay.Player2,200);
                                mBoxes.Add(box);
                                x -= bw;
                            }
                            y += bh;
                        }
                        x = 233;
                        y = 5.8f;
                        for (int i = 0; i < 4; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "block2", true, gameplay.Player2,200);
                            mBoxes.Add(box);
                            x -= bw;
                        }
                        x = 233;
                        y = 8.8f;
                        for (int i = 0; i < 3; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(world, position, size, "block2", true, gameplay.Player2,200);
                            mBoxes.Add(box);
                            x -= bw + bw / 2f;
                        }
                        y = -18f;
                        for (int i = 0; i < 5; i++)
                        {
                            x = 236;
                            for (int j = 0; j < 8; j++)
                            {
                                position = new Vector2(x, y);
                                size = new Vector2(bw, bh);
                                box = new Box(world, position, size, "block3", true, gameplay.Player2);
                                mBoxes.Add(box);
                                x += bw;
                            }
                            y += bh;
                        }
                        break;
                    }
            }
            return mBoxes;
        }
    }
}
