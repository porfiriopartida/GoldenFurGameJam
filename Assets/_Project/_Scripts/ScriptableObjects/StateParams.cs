using System;
using System.Collections.Generic;
using GoldenFur.Character;
using GoldenFur.Common;

namespace GoldenFur.ScriptableObjects
{
    [Serializable]
    public class StateParams
    {
        public PlayerState state;
        public CollisionParameters collisionParameters;
    }
}