using UnityEngine;
using UnityEngine.UIElements;

public class BolaMathZ
{

    public static Vector2[] GenerateCirclePointMap(float h, float k, float r, int resolution)
    {
        Vector2[] pointMap = new Vector2[resolution];

        for (int i = 0; i < resolution; i++)
        {
            float angle = 2 * Mathf.PI * i / resolution; // Angle in radians
            float[] sinCosTan = CalculateSinCosFromRadian(angle); // Get sin, cos, and tan values

            Vector2 point = new Vector2(sinCosTan[0], sinCosTan[1]) * r; // Scale by radius

            point += new Vector2(h, k); // Translate to (h, k)

            pointMap[i] = point;
        }

        return pointMap;
    }

    /// <summary>
    /// Generates a point map for a parabola given its vertex (h, k), focus distance p, orientation (vertical or horizontal), and resolution. uses (y−k)=a(x−h)^2 in the form y=a(x-h)^2 + k
    /// </summary>
    /// <param name="h">X position for the parabola</param>
    /// <param name="k">Y position for the parabola</param>
    /// <param name="p">Focus distance for the parabola</param>
    /// <param name="isVertical">Sets whether the parabola is calculated sideways or vertically</param>
    /// <param name="resolution">Amount of points generated</param>
    /// <returns>The parabola's point map</returns>
    public static Vector2[] GenerateParabolaPointMap(float h, float k, float a, bool isVertical, int resolution, float scale)
    {
        Vector2[] pointMap = new Vector2[resolution];

        switch (isVertical)
        {
            case true:

                int startX = -resolution / 2; // Centering the parabola around the origin
                int endX = resolution / 2;

                // For vertical parabola: y = a(x - h)^2 + k
                for (int x = startX; x < endX; x++)
                {
                    float yValue = a * Mathf.Pow((x - h), 2) + k;

                    pointMap[x + resolution / 2] = new Vector2(x, yValue) * scale;
                }

                break;

            case false:

                int startY = -resolution / 2; // Centering the parabola around the origin
                int endY = resolution / 2;

                // For horizontal parabola: x = a(y - h)^2 + k
                for (int y = startY; y < endY; y++)
                {
                    float xValue = a * Mathf.Pow((y - h), 2) + k;

                    pointMap[y + resolution / 2] = new Vector2(xValue, y) * scale;
                }

                break;
        }

        return pointMap;
    }

    public static Vector2[] GenerateEllipsePointMap(float h, float k, float a, float b, int resolution, float scale)
    {
        Vector2[] pointMap = new Vector2[resolution];

        for (int i = 0; i < resolution; i++)
        {
            float theta = 2 * Mathf.PI * i / (resolution - 1);

            float x = h + a * Mathf.Cos(theta);
            float y = k + b * Mathf.Sin(theta);

            pointMap[i] = new Vector2(x, y) * scale;
        }

        return pointMap;

    }

    public static Vector2[] GenerateHyperbolaPointMap(float h, float k, float a, float b, float c, bool verticalOrHorizontal, int resolution)
    {
        return new Vector2[] { new Vector2(0, 0) }; // Temp
    }

    /// <summary>
    /// Calculates sine, cosine, and tangent values from an angle in radians.
    /// </summary>
    /// <param name="radians">Angle in radians.</param>
    /// <returns>Array where [0] is sin, [1] is cos, [2] is tan.</returns>
    public static float[] CalculateSinCosFromRadian(float radians)
    {
        float[] sinCosTan = new float[3];

        sinCosTan[0] = Mathf.Sin(radians);
        sinCosTan[1] = Mathf.Cos(radians);

        return sinCosTan; 
    }
}
