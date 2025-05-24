using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 패턴, 스테틱은 인스펙터 표시X
    public PollManager pool;
    public Player player;

    void Awake()
    {
        instance = this; // 싱글톤 패턴
    }
}
