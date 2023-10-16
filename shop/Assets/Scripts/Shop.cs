using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    public Animator anim;

    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform[] itemPos;




    Player enterPlayer;

    public void Enter(Player player)
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    public void Exit()
    {
        anim.SetTrigger("doHello");
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }

    public void Buy(int index)
    {
        // 아이템을 초기 UI 위치를 기준으로 생성
        Vector3 ranVec = Vector3.right * Random.Range(-3, 3)
                        + Vector3.forward * Random.Range(-3, 3);

        // 아이템의 높이 조정 (예: 1.0f 를 더해줍니다)
        ranVec.y = 1.0f; // 원하는 높이로 조정하세요

        GameObject newItem = Instantiate(itemObj[index], itemPos[index].position + ranVec, itemPos[index].rotation);

        // Rigidbody 컴포넌트를 찾아서 비활성화합니다.
        Rigidbody itemRigidbody = newItem.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
        {
            itemRigidbody.isKinematic = false;
        }
    }








}