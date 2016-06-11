﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrazyStorm_Player.CrazyStorm
{
    class ParticleQuadTree
    {
        private const int MaxDepth = 4;
        public List<ParticleBase> Particles { get; private set; }
        public ParticleQuadTree[] children { get; private set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int OriginX { get; private set; }
        public int OriginY { get; private set; }
        public ParticleQuadTree(int left, int right, int top, int bottom)
        {
            children = new ParticleQuadTree[4];
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
            OriginX = (Left + Right) / 2;
            OriginY = (Top + Bottom) / 2;
        }
        public void Insert(ParticleBase particleBase, int depth = 0)
        {
            if (depth >= MaxDepth)
            {
                if (Particles == null)
                    Particles = new List<ParticleBase>();

                Particles.Add(particleBase);
                particleBase.QuadTree = this;
                return;
            }
            var x = particleBase.PPosition.x - OriginX;
            var y = particleBase.PPosition.y - OriginY;
            if (x > 0 && y > 0)
            {
                if (children[0] == null)
                    children[0] = new ParticleQuadTree(OriginX, Right, Top, OriginY);

                children[0].Insert(particleBase, depth + 1);
            }
            else if (x > 0 && y <= 0)
            {
                if (children[1] == null)
                    children[1] = new ParticleQuadTree(OriginX, Right, OriginY, Bottom);

                children[1].Insert(particleBase, depth + 1);
            }
            else if (x <= 0 && y <= 0)
            {
                if (children[2] == null)
                    children[2] = new ParticleQuadTree(Left, OriginX, OriginY, Bottom);

                children[2].Insert(particleBase, depth + 1);
            }
            else
            {
                if (children[3] == null)
                    children[3] = new ParticleQuadTree(Left, OriginX, Top, OriginY);

                children[3].Insert(particleBase, depth + 1);
            }
        }
        public List<ParticleBase> SearchByRect(int left, int right, int top, int bottom)
        {
            List<ParticleBase> results = new List<ParticleBase>();
            if (Particles != null)
            {
                results.AddRange(Particles);
                return results;
            }
            if (children[0] != null && right > children[0].OriginX && bottom > children[0].OriginY)
                results.AddRange(children[0].SearchByRect(left, right, top, bottom));
            
            if (children[1] != null && right > children[1].OriginX && top <= children[1].OriginY)
                results.AddRange(children[1].SearchByRect(left, right, top, bottom));
            
            if (children[2] != null && left <= children[2].OriginX && top <= children[2].OriginY)
                results.AddRange(children[2].SearchByRect(left, right, top, bottom));
            
            if (children[3] != null && left <= children[3].OriginX && bottom > children[3].OriginY)
                results.AddRange(children[3].SearchByRect(left, right, top, bottom));
            
            return results;
        }
        public void Update(ParticleBase particleBase)
        {
            if (particleBase.PPosition.x < Left || particleBase.PPosition.x > Right || 
                particleBase.PPosition.y < Top || particleBase.PPosition.y > Bottom)
            {
                Particles.Remove(particleBase);
                ParticleManager.ParticleQuadTree.Insert(particleBase);
            }
        }
    }
}