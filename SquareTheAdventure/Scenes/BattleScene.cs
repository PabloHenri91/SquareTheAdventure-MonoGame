using System;
using System.Collections.Generic;
using System.Text;

using SquareTheAdventure;
using SquareTheAdventure.Map;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hydra.Scenes
{
    class BattleScene : SKScene
    {
        MapManager mapManager;
        Player player;

        internal override void load()
        {
            base.load();

            camera = new CameraNode();
            mapManager = new MapManager(0, 0);
            player = new Player();

            gameWorld.addChild(camera);
            gameWorld.addChild(mapManager);
            gameWorld.addChild(player);

            player.loadPhysics();

            mapManager.reloadMap();
        }

        internal override void update()
        {
            base.update();

            mapManager.update(player.position);

            KeyboardState keyboardState = Keyboard.GetState();

            bool left = false;
            bool right = false;
            bool up = false;

            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                switch (key)
                {
                    case Keys.A:
                    case Keys.Left:
                        left = true;
                        break;
                    case Keys.D:
                    case Keys.Right:
                        right = true;
                        break;
                    case Keys.Up:
                    case Keys.W:
                    case Keys.Space:
                        up = true;
                        break;
                    default:
                        Console.WriteLine(key);
                        break;
                }
            }

            player.update(left, right, up);

            camera.position = Vector2.Lerp(camera.position, player.position, 0.1f);
        }

        internal override void touchDown(Touch touch)
        {
            base.touchDown(touch);
        }

        internal override void touchMoved(Touch touch)
        {
            base.touchMoved(touch);
        }

        internal override void touchUp(Touch touch)
        {
            base.touchUp(touch);
        }
    }
}
