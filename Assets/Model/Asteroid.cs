﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Model
{
    public struct Asteroid
    {
        public Vector3 Position { get; set; }
        public Vector3 NewPosition { get; set; }
        public float Radius { get; set; }
        public Quaternion Rotation { get; set; }
    }
}
