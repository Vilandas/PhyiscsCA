using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using GDLibrary.Interfaces;
using GDLibrary.Managers;
using GDLibrary.Actors;
using GDLibrary.Enums;
using GDGame.MyGame.Constants;
using GDLibrary.Events;

namespace GDGame.MyGame.Controllers
{
    public class ThirdPersonFollowCam : IController
    {
        #region Fields

        private KeyboardManager keyboardManager;
        private MouseManager mouseManager;
        private Camera3D camera3D;

        #endregion

        #region Constructors

        public ThirdPersonFollowCam(KeyboardManager keyboardManager,
            MouseManager mouseManager,
            Camera3D camera3D)
        {
            this.keyboardManager = keyboardManager;
            this.mouseManager = mouseManager;
            this.camera3D = camera3D;
        }

        #endregion

        public void Update(GameTime gameTime, IActor actor)
        {
            Actor3D parent = actor as Actor3D;

            if (parent != null)
            {
                HandleCameraFollow(gameTime, parent);
                HandleMouseInput(gameTime);
            }
        }

        private void HandleCameraFollow(GameTime gameTime, Actor3D parent)
        {
            //Offest the objects position to where the camera should be
            Vector3 parentPos = parent.Transform3D.Translation;
            parentPos.X += 30;
            parentPos.Y += 15;
            parentPos.Z += 30;

            //subtract objects position from camera position to get the distance
            parentPos -= camera3D.Transform3D.Translation;

            //Offset the position before adding so it will take several updates to move to the objects position (This make the camera move smoothly)
            //parentPos *= 0.1f;
            camera3D.Transform3D.Translation += parentPos;
        }

        private void HandleMouseInput(GameTime gameTime)
        {
            Vector2 mouseDelta = mouseManager.GetDeltaFromCentre(new Vector2(720, 525));
            mouseDelta *= -0.01f * gameTime.ElapsedGameTime.Milliseconds;

            if (mouseDelta.Length() != 0)
            {
                camera3D.Transform3D.RotateBy(new Vector3(mouseDelta.X, 0, -mouseDelta.Y));
            }
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public ControllerType GetControllerType()
        {
            throw new NotImplementedException();
        }
    }
}
