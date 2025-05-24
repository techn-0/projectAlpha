using System.Collections.Generic;
using UnityEngine;

public class PollManager : MonoBehaviour
{
    // 프리팹 보관할 변수
    public GameObject[] prefabs;


    // 풀 담담하는 리스트
    List<GameObject>[] pools; // Poll 프리팹을 담는 리스트

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; // 프리팹의 개수만큼 리스트 생성

        for (int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>(); // 각 프리팹에 대한 리스트 초기화
        }
    }
    
    public GameObject GetObject(int index)
    {
        if (index < 0 || index >= pools.Length) // 인덱스가 범위를 벗어나면
        {
            Debug.LogError("Index out of range");
            return null;
        }

        GameObject select = null;

        // 선택한 풀의 놀고있는 게임 오브젝트 접근
        // 발견시 select에 할당
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf) // 비활성화된 오브젝트 찾기
            {
                select = item; // 선택된 오브젝트 할당
                select.SetActive(true); // 오브젝트 활성화
                break; // 찾았으면 루프 종료
            }
        }
        // 못찾았으면 새로 생성 후 select에 할당
        if (select == null)
        {
            select = Instantiate(prefabs[index], transform); // 프리팹 인스턴스화, Instantiate: 원본 오브젝트 복제해 장면에 생성하는 함수
            pools[index].Add(select); // 풀에 추가
        }
        return select;
    }

}
