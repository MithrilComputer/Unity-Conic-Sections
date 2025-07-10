using UnityEngine;

public class RenderingManager : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public void RenderPointMap(Vector2[] pointMap, Vector2 offset, bool closeLoop)
    {
        if (pointMap == null || pointMap.Length == 0)
        {
            Debug.LogWarning("Point map is empty or null.");
            return;
        }

        lineRenderer.loop = closeLoop;

        lineRenderer.positionCount = pointMap.Length;

        for (int i = 0; i < pointMap.Length; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(pointMap[i].x, pointMap[i].y, 0f) + new Vector3(offset.x, offset.y, 0f));
        }
    }
}
