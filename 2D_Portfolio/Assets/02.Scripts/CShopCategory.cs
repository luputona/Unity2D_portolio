﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CShopCategory : SingleTon<CShopCategory> // 임시로 싱글턴, 추후 다시 모노로 변경
{
    public enum ESelcetCategory
    {
        Sword,
        Bow,
        Staff,
        Accessory,
        Mace,
        Spear,
        MatialArts,
        Default = 99
    }

    [SerializeField]
    private GameObject m_shop_Catergory;
    [SerializeField]
    private GameObject m_shopCategoryBtn;
    [SerializeField]
    private CTouchSpriteCheck m_shopcheck;
    [SerializeField]
    private Text m_categoryText;
    [SerializeField]
    private Button m_categoryBtn;

    public bool m_isCategoryBtnColor = true;
    public ESelcetCategory m_eSelcetCategory = ESelcetCategory.Default;
    public List<GameObject> m_categorySlotList = new List<GameObject>();
    public int m_categoryCount;
    public GameObject m_categoryslotPrefab;

    public CStaffData cStaffData;
    private void Awake()
    {
        m_shop_Catergory = GameObject.Find("Shop_Catergory").gameObject;
        m_shopcheck = gameObject.GetComponent<CTouchSpriteCheck>();
        
    }
    void Start()
    {
        m_categoryCount = CWeaponData.GetInstance.m_categoryLocalList.Count;
        PopupCategoryPanel();
    }

    private void Update()
    {
        

    }

    void PopupCategoryPanel()
    {
        for(int i = 0; i < m_categoryCount; i++)
        {
            m_categorySlotList.Add(Instantiate(m_categoryslotPrefab));
            m_categorySlotList[i].transform.SetParent(m_shop_Catergory.transform,false);
            m_categorySlotList[i].transform.name = CWeaponData.GetInstance.m_categoryLocalList[i].m_category; // TODO: 추후 m_categoryLocalList를 m_categoryList 로 변경
            m_categoryText = m_categorySlotList[i].transform.GetChild(0).GetComponent<Text>();
            m_categoryText.text = string.Format("{0}" ,CWeaponData.GetInstance.m_categoryLocalList[i].m_category);
        }
        m_categoryBtn = m_categorySlotList[0].gameObject.GetComponent<Button>();
        if (m_eSelcetCategory == ESelcetCategory.Default)
        {
            ColorBlock cb = m_categoryBtn.colors;
            cb.normalColor = new Color32(124,124,124,255);
            m_categoryBtn.colors = cb;
        }        
    }
    
    public void CheckedCategoryButton()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);

                if (hit)
                {
                    if(hit.transform.gameObject.name == "" )
                    {

                    }
                }
            }
        }
    }

    public void ButtonColorSetting()
    {

    }


}
