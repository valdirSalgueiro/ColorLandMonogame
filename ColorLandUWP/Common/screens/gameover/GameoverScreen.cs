﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using ColorLandUWP;

namespace ColorLand
{
    class GameoverScreen : BaseScreen
    {
        private const String cSOUND_HIGHLIGHT = "sound\\fx\\highlight8bit";
        private SpriteBatch mSpriteBatch;

        //Lista dos backgrounds
        private Background mBackgroundImage;

        private Background mCurrentBackground;

        private List<Background> mList = new List<Background>();

        private const int cMAX_BG_COUNTER = 3;
        private int mBackgroundCounter;

        private Button mCurrentHighlightButton;
        private bool mMousePressing;

        private Texture2D mGameoverTitleTexture;
        private Texture2D mGameoverBackgroundTexture;

        //fade
        private Fade mFade;
        private Fade mCurrentFade;


        /***
         * BUTTONS
         * */
        private Button mButtonYes;
        private Button mButtonNo;

        private GameObjectsGroup<Button> mGroupButtons;

        public GameoverScreen()
        {

            if (!SoundManager.isPlaying())
            {
                SoundManager.PlayMusic("sound\\music\\theme");
            }
            mSpriteBatch = Game1.getInstance().getScreenManager().getSpriteBatch();

            mBackgroundImage = new Background("mainmenu\\mainmenubg");
            mBackgroundImage.loadContent(Game1.getInstance().getScreenManager().getContent());

            mList.Add(mBackgroundImage);

            mCurrentBackground = mList.ElementAt(0);

            mCursor = new Cursor();
            mCursor.loadContent(Game1.getInstance().getScreenManager().getContent());

            mButtonYes = new Button("gameplay\\pausescreen\\continue", "gameplay\\pausescreen\\continue_select", "gameplay\\pausescreen\\continue_selected", new Rectangle(400 - 319 / 2, 300 - 117/2 - 50, 319, 117));
            mButtonNo = new Button("gameplay\\pausescreen\\exit", "gameplay\\pausescreen\\exit_select", "gameplay\\pausescreen\\exit_selected", new Rectangle(400 - 290 / 2, 300 - 115 / 2 + 80, 290, 115));

            //mPauseTitleTexture = Game1.getInstance().getScreenManager().getContent().Load<Texture2D>("gameplay\\pausescreen\\paused_title");
            mGameoverBackgroundTexture = Game1.getInstance().getScreenManager().getContent().Load<Texture2D>("fades\\blackfade");

            mGroupButtons = new GameObjectsGroup<Button>();
            //mGroupButtons.addGameObject(mButtonContinue);
            mGroupButtons.addGameObject(mButtonYes);
            mGroupButtons.addGameObject(mButtonNo);
            
            mGroupButtons.loadContent(Game1.getInstance().getScreenManager().getContent());

            //mButtonPlay.loadContent(Game1.getInstance().getScreenManager().getContent());
            //mButtonHelp.loadContent(Game1.getInstance().getScreenManager().getContent());
            //mButtonCredits.loadContent(Game1.getInstance().getScreenManager().getContent());            

            //Game1.print("LOC: "  + mGroupButtons.getGameObject(2).getLocation());

            SoundManager.LoadSound(cSOUND_HIGHLIGHT);

        }


        public override void update(GameTime gameTime)
        {
            //checkCollisions();
            mCurrentBackground.update();
            mGroupButtons.update(gameTime);
            mCursor.update(gameTime);
            updateMouseInput();
            checkCollisions();

            if (mFade != null)
            {
                //mFade.update(gameTime);
            }
        }


        public override void draw(GameTime gameTime)
        {
            mSpriteBatch.Begin();
            //mCurrentBackground.draw(mSpriteBatch);

            mSpriteBatch.Draw(mGameoverBackgroundTexture, new Rectangle(0, 0, 800, 600), new Color(0, 0, 0, 0.5f));

            //mSpriteBatch.Draw(mGameoverTitleTexture, new Rectangle(150, 0, 577, 222), Color.White);
            
            mGroupButtons.draw(mSpriteBatch);
            mCursor.draw(mSpriteBatch);

            /*if (mFade != null)
            {
                mFade.draw(mSpriteBatch);
            }*/

            mSpriteBatch.End();

        }

        private void updateMouseInput()
        {


            var ms = Game1.getMousePosition();

            if (ms.LeftButton == ButtonState.Pressed)
            {
                mMousePressing = true;
            }
            else
            {
                if (mCurrentHighlightButton != null)
                {

                    if (mMousePressing)
                    {
                        processButtonAction(mCurrentHighlightButton);
                    }

                }

                mMousePressing = false;
            }


        }

        //hehehehehe
        private void solveHighlightBug()
        {
            if (mCurrentHighlightButton != null)
            {
                for (int x = 0; x < mGroupButtons.getSize(); x++ )
                {

                    Button b = mGroupButtons.getGameObject(x);

                    if(b != mCurrentHighlightButton){

                        b.changeState(Button.sSTATE_NORMAL);

                    }

                }

            }
        }

        private void checkCollisions()
        {

            if (mGroupButtons.checkCollisionWith(mCursor))
            {
                mCurrentHighlightButton = (Button)mGroupButtons.getCollidedObject();

                solveHighlightBug();

                if (mMousePressing)
                {
                    if (mCurrentHighlightButton.getState() != Button.sSTATE_PRESSED)
                    {
                        mCurrentHighlightButton.changeState(Button.sSTATE_PRESSED);
                    }
                }
                else
                {

                    if (mCurrentHighlightButton.getState() != Button.sSTATE_HIGHLIGH)
                    {
                        mCurrentHighlightButton.changeState(Button.sSTATE_HIGHLIGH);
                    }

                }
                
            }else{
                if (mCurrentHighlightButton != null)// && mCurrentHighlightButton.getState() != Button.sSTATE_PRESSED)
                {
                    mCurrentHighlightButton.changeState(Button.sSTATE_NORMAL);
                }
                mCurrentHighlightButton = null;
            }

        }

        private void processButtonAction(Button button)
        {
            if (button == mButtonYes)
            {
                //SoundManager.PlaySound(cSOUND_HIGHLIGHT);
                //mFade = new Fade(this, "fades\\blackfade");
                //executeFade(mFade, Fade.sFADE_OUT_EFFECT_GRADATIVE);
            }
            if (button == mButtonNo)
            {
                //SoundManager.PlaySound(cSOUND_HIGHLIGHT);
                //mFade = new Fade(this, "fades\\blackfade");
                //executeFade(mFade, Fade.sFADE_OUT_EFFECT_GRADATIVE);
                Game1.getInstance().getScreenManager().changeScreen(ScreenManager.SCREEN_ID_MAIN_MENU, true,true);
            }
           
        }

        //timer
        /*private void restartTimer()
        {
            mTimerFade = new MTimer();
            mTimerFade.start();
        }
        
      
        private void updateTimer(GameTime gameTime)
        {
            if (mTimerFade != null)
            {

                mTimerFade.update(gameTime);

                if (mTimerFade.getTimeAndLock(3))
                {

                }

            }
        }*/


    }
}
