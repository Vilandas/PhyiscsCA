using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GDGame.MyGame.Constants
{
    public class GameConstants
    {
        #region Common

        public static readonly Keys[][] MoveKeys = { 
            new Keys[]{ Keys.W, Keys.S, Keys.A, Keys.D },
            new Keys[]{ Keys.Up, Keys.Down, Keys.Left, Keys.Right }
        };

        #endregion Common

        #region Third Person Player Camera

        public static readonly Vector3 playerCamLook = new Vector3(0, -1, -1);
        public static readonly Vector3 playerCamLookInverted = new Vector3(0, 1, 1);

        #endregion

        #region First Person Camera

        public static readonly float moveSpeed = 0.1f;
        public static readonly float strafeSpeed = 0.075f;
        public static readonly float rotateSpeed = 0.01f;

        #endregion First Person Camera

        #region Flight Camera

        public static readonly float flightMoveSpeed = 0.8f;
        public static readonly float flightStrafeSpeed = 0.6f;
        public static readonly float flightRotateSpeed = 0.01f;

        #endregion Flight Camera

        #region Security Camera

        private static readonly float angularSpeedMultiplier = 10;
        public static readonly float lowAngularSpeed = 10;
        public static readonly float mediumAngularSpeed = lowAngularSpeed * angularSpeedMultiplier;
        public static readonly float hiAngularSpeed = mediumAngularSpeed * angularSpeedMultiplier;

        #endregion Security Camera

        #region Car

        public static readonly float carMoveSpeed = 0.08f;
        public static readonly float carRotateSpeed = 0.06f;

        #endregion Car

        #region Player

        public static readonly float playerMoveSpeed = 0.1f;
        public static readonly float playerRotateSpeed = 4f;

        public static readonly float playerCamOffsetY = 300;
        public static readonly float playerCamOffsetZ = 300;

        public static readonly Vector3 playerHoldPos = new Vector3(32, 40, 3);
        public static readonly Vector3 potionRedPos = new Vector3(4, 18, -1.5f);

        public static readonly Keys[] playerInteractKeys = { Keys.Space, Keys.RightControl };
        public static readonly Buttons[] playerInteractButtons = { Buttons.LeftTrigger };
        public static readonly float defualtInteractionDist = 50f;

        #endregion

        #region Objects

        public static readonly Vector3 cauldronPos = new Vector3(100, 0, 100);
        public static readonly Vector3 binPos = new Vector3(-100, 0, 100);

        #endregion
    }
}