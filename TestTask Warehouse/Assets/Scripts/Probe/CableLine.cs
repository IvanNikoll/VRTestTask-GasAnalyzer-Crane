using UnityEngine;

public class CableLine : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform _floor;
    [SerializeField] private int segments = 10;
    [SerializeField] private float sagAmount = 0.002f;

    private LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segments;
    }

    void LateUpdate()
    {
        if (!startPoint || !endPoint) return;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            Vector3 pointPos = Vector3.Lerp(startPoint.position, endPoint.position, t);

            float sag = Mathf.Sin(Mathf.PI * t) * sagAmount;
            pointPos.y -= sag;

            if (pointPos.y < _floor.position.y)
                pointPos.y = _floor.position.y;

            line.SetPosition(i, pointPos);
        }
    }
}