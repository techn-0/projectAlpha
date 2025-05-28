using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; // 스캔 범위
    public LayerMask targetLayer; // 타겟 레이어
    public RaycastHit2D[] targets; // 타겟 배열
    public Transform nearestTargets; // 가장 가까운 타겟 배열

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTargets = GetNearest(); // 가장 가까운 타겟을 찾음
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;
        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos); // 현재 거리 계산

            if (curDiff < diff) // 현재 거리가 이전 거리보다 작으면
            {
                diff = curDiff; // 현재 거리를 새로운 최소 거리로 설정
                result = target.transform; // 가장 가까운 타겟으로 설정
            }
        }
        

        return result;
    }
}
