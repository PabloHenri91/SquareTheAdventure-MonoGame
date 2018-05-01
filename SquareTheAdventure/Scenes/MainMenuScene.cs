using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace Hydra.Scenes
{
    class MainMenuScene : SKScene
    {

        enum State
        {
            mainMenu,
            battle
        }

        State state = State.mainMenu;
        State nextState = State.mainMenu;

        Button buttonPlay;

        internal override void load()
        {
            base.load();
            buttonPlay = new Button("yellow_button01", 100, 100);
            addChild(buttonPlay);
        }

        internal override void update()
        {
            base.update();

            if (state == nextState)
            {
                switch (state)
                {
                    case State.mainMenu:
                        break;
                    case State.battle:
                        break;
                }
            }
            else
            {
                switch (nextState)
                {
                    case State.mainMenu:
                        break;
                    case State.battle:
                        presentScene(new BattleScene());
                        break;
                    default:
                        break;
                }
                state = nextState;
            }
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

            if (state == nextState)
            {
                switch (state)
                {
                    case State.mainMenu:
                        if (buttonPlay.state == ButtonState.Pressed &&
                            buttonPlay.contains(touch.locationIn(buttonPlay.parent)))
                        {
                            nextState = State.battle;
                        }
                        break;
                    case State.battle:
                        break;
                }
            }
        }
    }
}
