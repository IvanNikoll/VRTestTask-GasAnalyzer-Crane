using UnityEngine;

public class CableLine : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private int segments = 10;
    [SerializeField] private float sagAmount = 1;

    private LineRenderer line;

    public void Initialize(Transform startPoint, Transform endPoint)
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segments;
        _endPoint = endPoint;
        _startPoint = startPoint;
    }

    void LateUpdate()
    {
        if (!_startPoint || !_endPoint) return;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            Vector3 pointPos = Vector3.Lerp(_startPoint.position, _endPoint.position, t);

            float sag = Mathf.Sin(Mathf.PI * t) * sagAmount;
            pointPos.y -= sag;

            line.SetPosition(i, pointPos);
        }
    }
}