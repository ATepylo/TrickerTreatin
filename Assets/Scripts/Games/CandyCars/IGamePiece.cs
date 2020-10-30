using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandyCars
{
    public class GamePiece : MonoBehaviour
    {
        public bool isPlay = false;
        public virtual void Activate() => isPlay = true;
        public virtual void Deactivate() => isPlay = false;
    }
}


