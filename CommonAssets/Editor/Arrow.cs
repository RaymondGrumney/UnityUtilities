using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CommonAssets.Editor
{
    class Arrow
    {
        public static void Single(Vector2 from, Vector2 to, float arrowHeadLength = 0.5f)
        {
            Gizmos.DrawLine(from, to);
            Gizmos.DrawLine(to, to + (Vector2.up + Vector2.right * arrowHeadLength));
            Gizmos.DrawLine(to, to + (Vector2.up + Vector2.left * arrowHeadLength));
        }
        public static void Single(Vector2 from, Vector2 to, Color color, float arrowHeadLength = 0.5f)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(from, to);
            Gizmos.DrawLine(to, to + (Vector2.up + Vector2.right * arrowHeadLength));
            Gizmos.DrawLine(to, to + (Vector2.up + Vector2.left * arrowHeadLength));
        }

        public static void Double(Vector2 from, Vector2 to, float arrowHeadLength = 0.5f)
        {
            Gizmos.DrawLine(from, to);
            Gizmos.DrawLine(to, to + (Vector2.up + Vector2.right * arrowHeadLength));
            Gizmos.DrawLine(to, to + (Vector2.up + Vector2.left * arrowHeadLength));
            Gizmos.DrawLine(from, from + (Vector2.up + Vector2.right * arrowHeadLength));
            Gizmos.DrawLine(from, from + (Vector2.up + Vector2.left * arrowHeadLength));
        }

        public static void Double(Vector2 from, Vector2 to, Color color, float arrowHeadLength = 0.5f)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(from, to);
            Gizmos.DrawLine(to, to + (Vector2.up + Vector2.right * arrowHeadLength));
            Gizmos.DrawLine(to, to + (Vector2.down + Vector2.right * arrowHeadLength));
            Gizmos.DrawLine(from, from + (Vector2.up + Vector2.left * arrowHeadLength));
            Gizmos.DrawLine(from, from + (Vector2.down + Vector2.left * arrowHeadLength));
        }
    }
}
