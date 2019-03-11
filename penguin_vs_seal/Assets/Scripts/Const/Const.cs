using System;
using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;

namespace Const {

    public static class CO {
		public const float SPEED = 0.05f;

        public static readonly ReadOnlyCollection<float> CONST_LIST = 
                    Array.AsReadOnly(new float[] {1.0f, 2.0f, 3.0f});

    }

}