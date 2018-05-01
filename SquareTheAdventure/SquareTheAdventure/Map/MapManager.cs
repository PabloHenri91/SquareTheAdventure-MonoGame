using System;
using System.Collections.Generic;
using System.Text;

using Hydra;

using Microsoft.Xna.Framework;

namespace SquareTheAdventure.Map
{
    class MapManager : SKNode
    {
        Vector2 playerRegion;
        Vector2 loadedRegion;

        TiledMap[] chunks;

        bool loading;
        double lastUpdate;

        private int x;
        private int y;

        public MapManager(int x, int y)
        {
            chunks = new TiledMap[9];
            this.x = x;
            this.y = y;
        }

        internal void reloadMap()
        {
            removeAllChildren();

            int i = 0;

            for (int y = (int)playerRegion.Y - 1; y <= (int)playerRegion.Y + 1; y++)
            {
                for (int x = (int)playerRegion.X - 1; x <= (int)playerRegion.X + 1; x++)
                {
                    TiledMap chunk = new TiledMap(x, y);
                    chunks[i++] = chunk;
                }
            }

            addChunks();
        }

        internal void update(Vector2 position)
        {
            if (!loading)
            {
                if (SKScene.currentTime - lastUpdate > 0.1f)
                {
                    lastUpdate = SKScene.currentTime;

                    updatePlayerRegion(position);
                    if (playerRegion != loadedRegion)
                    {
                        loading = true;
                        loadMap();
                        loading = false;
                    }
                    else
                    {
                        foreach (TiledMap chunk in chunks)
                        {
                            chunk?.update();
                        }
                    }
                }
            }
        }

        private void updatePlayerRegion(Vector2 position)
        {
            playerRegion.X = (float)Math.Round(position.X / TiledMap.size.X, MidpointRounding.AwayFromZero);
            playerRegion.Y = (float)Math.Round(position.Y / TiledMap.size.Y, MidpointRounding.AwayFromZero);
        }

        private void loadMap()
        {
            if (playerRegion.X < loadedRegion.X)
            {
                loadedRegion.X = loadedRegion.X - 1;
                loadA();
                return;
            }
            if (playerRegion.Y > loadedRegion.Y)
            {
                loadedRegion.Y = loadedRegion.Y + 1;
                loadS();
                return;
            }
            if (playerRegion.X > loadedRegion.X)
            {
                loadedRegion.X = loadedRegion.X + 1;
                loadD();
                return;
            }
            if (playerRegion.Y < loadedRegion.Y)
            {
                loadedRegion.Y = loadedRegion.Y - 1;
                loadW();
                return;
            }
        }

        private void addChunks()
        {
            foreach (TiledMap chunk in chunks)
            {
                if (chunk.parent == null)
                {
                    addChild(chunk);

                    foreach (SKNode item in chunk.children)
                    {
                        if (item is TiledObject tiledObject)
                        {
                            tiledObject.loadPhysicsBody();
                        }
                    }
                }
            }
        }

        private void loadA()
        {
            chunks[2].removeFromParent();
            chunks[5].removeFromParent();
            chunks[8].removeFromParent();

            chunks[2] = chunks[1];
            chunks[5] = chunks[4];
            chunks[8] = chunks[7];

            chunks[1] = chunks[0];
            chunks[4] = chunks[3];
            chunks[7] = chunks[6];

            chunks[0] = new TiledMap(loadedRegion.X - 1, loadedRegion.Y - 1);
            chunks[3] = new TiledMap(loadedRegion.X - 1, loadedRegion.Y + 0);
            chunks[6] = new TiledMap(loadedRegion.X - 1, loadedRegion.Y + 1);

            addChunks();
        }

        private void loadS()
        {
            chunks[0].removeFromParent();
            chunks[1].removeFromParent();
            chunks[2].removeFromParent();

            chunks[0] = chunks[3];
            chunks[1] = chunks[4];
            chunks[2] = chunks[5];

            chunks[3] = chunks[6];
            chunks[4] = chunks[7];
            chunks[5] = chunks[8];

            chunks[6] = new TiledMap(loadedRegion.X - 1, loadedRegion.Y + 1);
            chunks[7] = new TiledMap(loadedRegion.X + 0, loadedRegion.Y + 1);
            chunks[8] = new TiledMap(loadedRegion.X + 1, loadedRegion.Y + 1);

            addChunks();
        }

        private void loadD()
        {
            chunks[0].removeFromParent();
            chunks[3].removeFromParent();
            chunks[6].removeFromParent();

            chunks[0] = chunks[1];
            chunks[3] = chunks[4];
            chunks[6] = chunks[7];

            chunks[1] = chunks[2];
            chunks[4] = chunks[5];
            chunks[7] = chunks[8];

            chunks[2] = new TiledMap(loadedRegion.X + 1, loadedRegion.Y - 1);
            chunks[5] = new TiledMap(loadedRegion.X + 1, loadedRegion.Y + 0);
            chunks[8] = new TiledMap(loadedRegion.X + 1, loadedRegion.Y + 1);

            addChunks();
        }

        private void loadW()
        {
            chunks[6].removeFromParent();
            chunks[7].removeFromParent();
            chunks[8].removeFromParent();

            chunks[6] = chunks[3];
            chunks[7] = chunks[4];
            chunks[8] = chunks[5];

            chunks[3] = chunks[0];
            chunks[4] = chunks[1];
            chunks[5] = chunks[2];

            chunks[0] = new TiledMap(loadedRegion.X - 1, loadedRegion.Y - 1);
            chunks[1] = new TiledMap(loadedRegion.X + 0, loadedRegion.Y - 1);
            chunks[2] = new TiledMap(loadedRegion.X + 1, loadedRegion.Y - 1);

            addChunks();
        }
    }
}
