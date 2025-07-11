using UnityEngine;

public class BolaMathZ
{
    /// <summary>
    /// Generates a point map for a circle given its center (h, k), radius r, and Point Map resolution.
    /// </summary>
    /// <param name="h">The center point x</param>
    /// <param name="k">The center point y</param>
    /// <param name="r">The radius of the circle</param>
    /// <param name="resolution">The amount of points generated.</param>
    /// <returns>The Generated Point Map.</returns>
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
    /// <param name="h">X position for the parabola.</param>
    /// <param name="k">Y position for the parabola.</param>
    /// <param name="a">Focus distance for the parabola.</param>
    /// <param name="isVertical">Sets whether the parabola is calculated sideways or vertically.</param>
    /// <param name="resolution">The amount of points generated.</param>
    /// <returns>The Generated Point Map.</returns>
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

    /// <summary>
    /// Generates a point map for an ellipse given its center (h, k), semi-major axis a, semi-minor axis b, resolution, and scale.
    /// The equation used is (x-h)^2/a^2 + (y-k)^2/b^2 = 1 in parametric form.
    /// </summary>
    /// <param name="h">X position for the ellipse.</param>
    /// <param name="k">Y position for the ellipse.</param>
    /// <param name="a">Thickness of the ellipse.</param>
    /// <param name="b">Height of the ellipse.</param>
    /// <param name="resolution">The amount of points generated.</param>
    /// <param name="scale">The value to scale all the points by.</param>
    /// <returns>The Generated Point Map.</returns>
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
    public static Vector2[][] GenerateHyperbolaPointMap(
        float h, float k, float a, float b,
        bool verticalOrHorizontal,
        float scale, int resolution, float tMax = 2.0f)
    {
        Vector2[] branch1 = new Vector2[resolution];
        Vector2[] branch2 = new Vector2[resolution];

        float step = (2f * tMax) / (resolution - 1);

        for (int i = 0; i < resolution; i++)
        {
            float t = -tMax + step * i;

            float cosh = (Mathf.Exp(t) + Mathf.Exp(-t)) / 2f;
            float sinh = (Mathf.Exp(t) - Mathf.Exp(-t)) / 2f;

            if (!verticalOrHorizontal) // Horizontal hyperbola
            {
                // Right branch
                float x1 = h + a * cosh;
                float y1 = k + b * sinh;
                branch1[i] = new Vector2(x1, y1) * scale;

                // Left branch
                float x2 = h - a * cosh;
                float y2 = k - b * sinh;
                branch2[i] = new Vector2(x2, y2) * scale;
            }
            else // Vertical hyperbola
            {
                // Upper branch
                float x1 = h + b * sinh;
                float y1 = k + a * cosh;
                branch1[i] = new Vector2(x1, y1) * scale;

                // Lower branch
                float x2 = h - b * sinh;
                float y2 = k - a * cosh;
                branch2[i] = new Vector2(x2, y2) * scale;
            }
        }

        return new Vector2[][] { branch1, branch2 };
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
