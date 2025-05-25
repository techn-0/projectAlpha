using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 패턴, 스테틱은 인스펙터 표시X

    public float gameTime; // 게임 시간
    public float maxGameTime = 2 * 10f; // 최대 게임 시간

    public PollManager pool;
    public Player player;

    void Awake()
    {
        instance = this; // 싱글톤 패턴
    }

    void Update()
    {
        gameTime += Time.deltaTime; // 타이머 증가

        if (gameTime >= maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
