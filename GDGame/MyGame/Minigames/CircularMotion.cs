using GDGame.MyGame.Constants;
using GDLibrary.Actors;
using GDLibrary.Interfaces;
using GDLibrary.Parameters;
using GDLibrary.Utilities;
using Microsoft.Xna.Framework;
using System;

namespace GDGame.MyGame.Minigames
{
    public class CircularMotion : IActor
    {
        private Transform3DCurve path;
        private ModelObject ball;
        private int radius;

        public CircularMotion()
        {
            Transform3DCurve path = new Transform3DCurve(CurveLoopType.Cycle);
            Vector3 rotation = new Vector3(0, -(float)Math.Atan2(GameConstants.playerCamLook.Z, GameConstants.playerCamLook.Y), 0);

            path.Add(new Vector3(0, 0, 0), -Vector3.UnitZ, Vector3.UnitY, 0);
            path.Add(MathUtility.RotateVec(new Vector3(radius, radius, 0), rotation), -Vector3.UnitZ, Vector3.UnitY, 1000);
            path.Add(MathUtility.RotateVec(new Vector3(0, 2* radius, 0), rotation), -Vector3.UnitZ, Vector3.UnitY, 2000);
            path.Add(MathUtility.RotateVec(new Vector3(-radius, radius, 0), rotation), -Vector3.UnitZ, Vector3.UnitY, 3000);
            path.Add(new Vector3(0, 0, 0), -Vector3.UnitZ, Vector3.UnitY, 4000);
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
