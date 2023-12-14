using UnityEngine;

// https://dev-tut.com/2022/unity-draw-a-circle-part2/

class Debug : UnityEngine.Debug
{
    public static void DrawCircle(Vector3 position, float radius, Color color)
    {
        int segments = 18;

        if (radius <= 0.0f || segments <= 0) { return; }

        float angleStep = (360.0f / segments);
        angleStep *= Mathf.Deg2Rad;

        Vector3 lineStart = Vector3.zero;
        Vector3 lineEnd = Vector3.zero;

        for (int i = 0; i < segments; i++)
        {
            lineStart.x = Mathf.Cos(angleStep * i) * radius;
            lineStart.y = Mathf.Sin(angleStep * i) * radius;

            lineEnd.x = Mathf.Cos(angleStep * (i + 1)) * radius;
            lineEnd.y = Mathf.Sin(angleStep * (i + 1)) * radius;

            lineStart += position;
            lineEnd += position;

            //DrawLine(position, lineEnd, color);
            DrawLine(lineStart, lineEnd, Color.blue);
        }
    }
}
