using System;
using GXPEngine.Core;
using GXPEngine.OpenGL;

namespace GXPEngine
{
    /// <summary>
    /// Implements an OpenGL line
    /// </summary>
    public class LineSegment : GameObject
    {
        private Arrow _normal;

        public Vec2 start;
        public Vec2 end;

        public uint color = 0xffffffff;
        public uint lineWidth = 1;

        public bool drawNormalArrow = false;

        public LineSegment(float pStartX, float pStartY, float pEndX, float pEndY, uint pColor = 0xffffffff, uint pLineWidth = 1, bool pDrawNormalArrow = false)
            : this(new Vec2(pStartX, pStartY), new Vec2(pEndX, pEndY), pColor, pLineWidth, pDrawNormalArrow)
        {
        }

        public LineSegment(Vec2 pStart, Vec2 pEnd, uint pColor = 0xffffffff, uint pLineWidth = 1, bool pDrawNormalArrow = false)
        {
            start = pStart;
            end = pEnd;
            color = pColor;
            lineWidth = pLineWidth;
            drawNormalArrow = pDrawNormalArrow;

            _normal = new Arrow(new Vec2(0, 0), new Vec2(0, 0), 40, 0xffff0000, 1);
            AddChild(_normal);
        }

        //------------------------------------------------------------------------------------------------------------------------
        //														RenderSelf()
        //------------------------------------------------------------------------------------------------------------------------
        override protected void RenderSelf(GLContext glContext)
        {
            if (game != null)
            {
                if (drawNormalArrow)
                {
                    recalculateArrowPosition();
                }

                Gizmos.RenderLine(start.x, start.y, end.x, end.y, color, lineWidth);
            }
        }

        private void recalculateArrowPosition()
        {
            _normal.startPoint = (start + end) * 0.5f;
            _normal.vector = (end - start).Normal();
        }
    }
}

