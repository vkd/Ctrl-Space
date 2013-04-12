using Microsoft.Xna.Framework;

namespace Ctrl_Space.Input
{
    interface IInputManager
    {
        void StartUpdate();
        void StopUpdate();
        void Update(GameTime gameTime);
        event InputAnalogEventHandler MoveUpDown;
        event InputAnalogEventHandler MoveRightLeft;
        event InputAnalogEventHandler Rotate;
        event InputDigitalEventHandler PrimaryWeapon;
        event InputDigitalEventHandler SecondaryWeapon;
        event InputPressEventHandler DebugMode;
        event InputPressEventHandler PlayStopMediaPlayer;
        event InputPressEventHandler ExitGame;
    }
}
