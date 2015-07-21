﻿/*
 * The MIT License (MIT)
 * Copyright (c) StarX 2015 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrazyStorm.Core
{
    public class Rebound : Component
    {
        #region Private Members
        int length;
        float rotation;
        int collisionTime;
        #endregion

        #region Public Members
        [IntProperty(0, int.MaxValue)]
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        [FloatProperty(float.MinValue, float.MaxValue)]
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        [IntProperty(0, int.MaxValue)]
        public int CollisionTime
        {
            get { return collisionTime; }
            set { collisionTime = value; }
        }
        #endregion

        #region Constructor
        public Rebound()
            : base()
        {
            Name = "NewRebound";
        }
        #endregion
    }
}