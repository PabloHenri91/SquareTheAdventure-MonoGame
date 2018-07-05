using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hydra.Scenes
{
    public class GameScene : SKScene
    {
        enum State
        {
            load,
            mainMenu
        }

        State state = State.load;
        State nextState = State.load;

        public GameScene()
        {
            Game1.samplerState = SamplerState.PointClamp;
            defaultSize = new Vector2(1280, 800);
            backgroundColor = new Color(22, 15, 38, 100);
        }

        internal override void load()
        {
            base.load();

            Control title;
            title = new Control("title", 455, 352);
            title.setAlignment(HorizontalAlignment.center, VerticalAlignment.center);
            addChild(title);

            Label label;

            label = new Label("Press Start", 
                              defaultSize.X / 2,
                              defaultSize.Y / 2 + 300,
                              HorizontalAlignment.center,
                              VerticalAlignment.center, 
                              FontName.kenPixel, 
                              FontSize.size16);
            addChild(label);
        }

        internal override void update()
        {
            base.update();

            if (state == nextState)
            {
                switch (state)
                {
                    case State.load:
                        break;
                    case State.mainMenu:
                        break;
                }
            }
            else
            {
                switch (nextState)
                {
                    case State.load:
                        break;
                    case State.mainMenu:
                        presentScene(new MainMenuScene());
                        break;
                }
                state = nextState;
            }
        }

        internal override void touchUp(Touch touch)
        {
            base.touchUp(touch);

            if (state == nextState)
            {
                switch (state)
                {
                    case State.load:
                        nextState = State.mainMenu;
                        break;
                    case State.mainMenu:
                        break;
                }
            }
        }
    }
}
