using Unity.VisualScripting;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D Coll;
    void Awake()
    {
        Coll = GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position; // 플레이어의 위치
        Vector3 myPos = transform.position; // 현재 오브젝트의 위치
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec; // 플레이어의 이동 방향
        float dirX = playerDir.x < 0 ? -1 : 1; // 플레이어가 왼쪽으로 이동 중이면 -1, 오른쪽이면 1
        float dirY = playerDir.y < 0 ? -1 : 1; // 플레이어가 아래로 이동 중이면 -1, 위로 이동 중이면 1

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if (Coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, -3f), Random.Range(-3f, -3f), 0f)); // 플레이어의 이동 방향으로 이동
                }

                break;
        }
    }
}
