using UnityEngine;

/// <summary>
/// Responsible for rendering point maps (such as conic sections) to the screen
/// using Unity's <see cref="LineRenderer"/> components.
/// Supports single and dual-curve rendering (e.g., hyperbola branches).
/// </summary>
public class RenderingManager : MonoBehaviour
{
    /// <summary>
    /// The primary <see cref="LineRenderer"/> used for drawing single-curve point maps or the first branch in dual rendering.
    /// </summary>
    [SerializeField] private LineRenderer lineRenderer1;

    /// <summary>
    /// The secondary <see cref="LineRenderer"/> used for drawing the second branch in dual-curve rendering.
    /// </summary>
    [SerializeField] private LineRenderer lineRenderer2;

    /// <summary>
    /// Renders a single point map (such as a circle, ellipse, or parabola) using <see cref="lineRenderer1"/>.
    /// Optionally closes the loop and applies a positional offset to all points.
    /// Clears <see cref="lineRenderer2"/>.
    /// </summary>
    /// <param name="pointMap">Array of points to render.</param>
    /// <param name="offset">Offset to apply to each point before rendering.</param>
    /// <param name="closeLoop">Whether to close the rendered loop (connect last point to first).</param>
    public void RenderPointMap(Vector2[] pointMap, Vector2 offset, bool closeLoop)
    {
        if (pointMap == null || pointMap.Length == 0)
        {
            Debug.LogWarning("Point map is empty or null.");
            return;
        }

        // Clear the secondary line renderer if not in use
        lineRenderer2.positionCount = 0;

        lineRenderer1.loop = closeLoop;
        lineRenderer1.positionCount = pointMap.Length;

        for (int i = 0; i < pointMap.Length; i++)
        {
            lineRenderer1.SetPosition(i, new Vector3(pointMap[i].x, pointMap[i].y, 0f) + new Vector3(offset.x, offset.y, 0f));
        }
    }

    /// <summary>
    /// Renders two point maps (such as both branches of a hyperbola) using <see cref="lineRenderer1"/> and <see cref="lineRenderer2"/>.
    /// Optionally closes the loops and applies a positional offset to all points.
    /// </summary>
    /// <param name="pointMap1">Array of points for the first branch or curve.</param>
    /// <param name="pointMap2">Array of points for the second branch or curve.</param>
    /// <param name="offset">Offset to apply to all points before rendering.</param>
    /// <param name="closeLoop">Whether to close the rendered loops (connect last point to first).</param>
    public void RenderDualPointMap(Vector2[] pointMap1, Vector2[] pointMap2, Vector2 offset, bool closeLoop)
    {
        if (pointMap1 == null || pointMap2 == null)
        {
            Debug.LogWarning("Point map is empty or null.");
            return;
        }

        lineRenderer1.loop = closeLoop;
        lineRenderer2.loop = closeLoop;

        lineRenderer1.positionCount = pointMap1.Length;
        lineRenderer2.positionCount = pointMap2.Length;

        for (int i = 0; i < pointMap1.Length; i++)
        {
            lineRenderer1.SetPosition(i, new Vector3(pointMap1[i].x, pointMap1[i].y, 0f) + new Vector3(offset.x, offset.y, 0f));
        }
        for (int i = 0; i < pointMap2.Length; i++)
        {
            lineRenderer2.SetPosition(i, new Vector3(pointMap2[i].x, pointMap2[i].y, 0f) + new Vector3(offset.x, offset.y, 0f));
        }
    }
}
