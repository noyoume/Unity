using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;

    public int coin;
    public int score;





    float hAxis;
    float vAxis;

    bool wDown;
    bool iDown;
    bool sDown1;
    bool sDown2;
    bool sDown3;

    Vector3 moveVec;

    Rigidbody rigid;
    Animator anim;

    GameObject nearObject;




    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Swap();
        Interaction();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        iDown = Input.GetButtonDown("Interaction");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    void Turn()
    {
        if (moveVec != Vector3.zero)
        {
            transform.LookAt(transform.position + moveVec);
        }
    }

    void Swap()
    {
        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

        if (sDown1 || sDown2 || sDown3)
        {
            weapons[weaponIndex].SetActive(true);
        }
    }

    void Interaction()
    {
        if (iDown && nearObject != null)
        {
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                if (item != null)
                {
                    int weaponIndex = item.value;
                    if (weaponIndex >= 0 && weaponIndex < hasWeapons.Length)
                    {
                        hasWeapons[weaponIndex] = true;
                        Destroy(nearObject);
                    }
                }
            }
            else if (nearObject.tag == "Shop")
            {
                Shop shop = nearObject.GetComponent<Shop>();
                if (shop != null)
                {
                    shop.Enter(this);
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Weapon") || other.CompareTag("Shop"))
        {
            nearObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            nearObject = null;
        }
        else if (other.CompareTag("Shop"))
        {
            Shop shop = other.GetComponent<Shop>();
            if (shop != null)
            {
                shop.Exit();
            }
            nearObject = null;
        }
    }
}