using ColorLandUWP;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Windows.UI.Xaml;
/* ***
* ESTA CLASSE GERENCIA OS LOGOS INICIAIS QUE DEVEM APARECER ANTES DO MENU PRINCIPAL
* 
* */

namespace ColorLand
{
    class LogosScreen : BaseScreen
    {
        private SpriteBatch mSpriteBatch;

        //Lista dos backgrounds
        private Background mBackgroundTeamImagineCup;
        private Background mBackgroundTeamLogo;

        private Background mCurrentBackground;

        private static Fade mFadeIn;

        private static Fade mCurrentFade;

        private List<Background> mList = new List<Background>();

        private static DispatcherTimer mTimer;

        private const int cMAX_BG_COUNTER = 2;
        private int mBackgroundCounter;
        

        public LogosScreen()
        {
            mSpriteBatch = Game1.getInstance().getScreenManager().getSpriteBatch();

            mBackgroundTeamImagineCup = new Background("logos\\imaginecup_bg");
            mBackgroundTeamImagineCup.loadContent(Game1.getInstance().getScreenManager().getContent());

            mBackgroundTeamLogo = new Background("logos\\chihuahuagameslogo");
            mBackgroundTeamLogo.loadContent(Game1.getInstance().getScreenManager().getContent());

            mList.Add(mBackgroundTeamImagineCup);
            mList.Add(mBackgroundTeamLogo);
            
            mCurrentBackground = mList.ElementAt(0);

            mFadeIn = new Fade(this, "fades\\blackfade");
            
            executeFade(mFadeIn,Fade.sFADE_IN_EFFECT_GRADATIVE);

        }


        private void nextBackground()
        {
            //Console.WriteLine("FUNCIONOU!!!!");
        }

        public override void update(GameTime gameTime)
        {
            mCurrentBackground.update();

            mFadeIn.update(gameTime);
           
        }

        public override void draw(GameTime gameTime)
        {
            mSpriteBatch.Begin();

            mCurrentBackground.draw(mSpriteBatch);

            mFadeIn.draw(mSpriteBatch);
            
            mSpriteBatch.End();
        }

        public override void executeFade(Fade fadeObject, int effect)
        {
            base.executeFade(fadeObject, effect);

            mCurrentFade = fadeObject;
            fadeObject.execute(effect);
        }

        ///////prototipo
        public override void fadeFinished(Fade fadeObject)
        {
            if(fadeObject.getEffect() == Fade.sFADE_IN_EFFECT_GRADATIVE){
                restartTimer(2);
            }else
            if (fadeObject.getEffect() == Fade.sFADE_OUT_EFFECT_GRADATIVE)
            {
                mBackgroundCounter++;
                if (mBackgroundCounter < cMAX_BG_COUNTER)
                {
                    mCurrentBackground = mList.ElementAt(mBackgroundCounter);
                    restartTimer(2);
                }
                else
                {
                    Game1.getInstance().getScreenManager().changeScreen(ScreenManager.SCREEN_ID_SPLASHSCREEN, true);
                }

            }
            
            //GC.KeepAlive(aTimer);

        }

        private void restartTimer(int seconds)
        {
            mTimer = new DispatcherTimer();
            mTimer.Tick += MTimer_Tick; ;
            mTimer.Interval = TimeSpan.FromSeconds(seconds);
            mTimer.Start();
        }

        private void MTimer_Tick(object sender, object e)
        {
            mTimer.Stop();

            if (mCurrentFade.getEffect() == Fade.sFADE_IN_EFFECT_GRADATIVE)
            {
                executeFade(mFadeIn, Fade.sFADE_OUT_EFFECT_GRADATIVE);
            }
            else

            if (mCurrentFade.getEffect() == Fade.sFADE_OUT_EFFECT_GRADATIVE)
            {
                executeFade(mFadeIn, Fade.sFADE_IN_EFFECT_GRADATIVE);
            }
        }

        //apenas limpar quando for para o menu principal
        private void clearAllBackgrounds()
        {

        }


    }
}
