using System;
using UnityEngine.Serialization;

namespace Models
{
    [Serializable]
    public class GameConfigurations
    {
        public float letterEnterAnimationDuration = 2;
        public float letterSelectedAnimationDuration = 2;
        public float letterPositionOffset = 0.5f;
        public float letterRotationOffset = 10;
        public int timePerEnvelopInSeconds = 10;
        public int timeForGameInSeconds = 10;
        
        
        public float envelopMoveFrontMailboxAnimationDuration = 2;
        public float envelopMoveInsideMailboxAnimationDuration = 2;
    }
}