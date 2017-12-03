using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorLandUWP.Common
{
    public class ProxyMouseState
    {
        public int X, Y;
        public int _scrollWheelValue;
        public ButtonState LeftButton;
        public ButtonState RightButton;
        public int _horizontalScrollWheelValue;
    }
}
