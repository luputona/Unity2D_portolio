﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CEntryDungeon : CVillageManager
{
    [SerializeField]
    private CItemShopSlotListManager m_ItemShopSlotListManager = null;
    [SerializeField]
    private CVillageManager m_cVillageManager = null;
    [SerializeField]
    private Text m_floorTitleText = null;
    public int m_curDungeonFloorIndex = 0;
    public Text m_itemDesc_Text = null;

    void Awake()
    {
        m_cVillageManager = this.GetComponent<CVillageManager>();
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        //TouchGetObj();
    }


    public void InitializeEntryDungeon()
    {
        m_ItemShopSlotListManager = this.GetComponent<CItemShopSlotListManager>();
        base.InitVillageManager();
    }

    public override void InsertShopDictionary()
    {
        base.InsertShopDictionary();
    }

    protected override void TouchGetObj()
    {
       
    }

    protected override void OpenShop()
    {

        if (m_shopinfo == ShopInfo.EntryDungeonDesk)
        {            
            m_itemDesc_Text = m_shopDictionary[ShopInfo.ItemDescription].gameObject.GetComponentInChildren<Text>();

            m_cShopCategory.m_eBackUiState = CSelectCategory.EBACKUISTATE.Closed;
            m_cShopCategory.ChangeSlotObjNameIsDungeonEntry();

            m_cShopCategory.SlotCount(1);

            //TODO : 추후 서버에 npc대사 모음으로 처리
            m_itemDesc_Text.text = string.Format("어서와 던전은 처음이야?? ");
        }
    }

    public void ShowFloorDataText(int tStart)
    {
        m_floorTitleText = m_ItemShopSlotListManager.m_slots[tStart].transform.GetChild(4).Find("inst_Dungeon_titleText").GetComponent<Text>();

        m_floorTitleText.text = string.Format(" 제 {0} 층", tStart + 1);
       
    }
    public void InsertFloorData(int tStartFloorIndex, int tEndFloorIndex)
    {
        
        for (int i = tStartFloorIndex; i < tEndFloorIndex; i++)
        {
            ShowFloorDataText(i);
        }
        ShowFloorInfo(tStartFloorIndex, tEndFloorIndex);
    }

    public void ShowFloorInfo(int tStartFloorIndex, int tEndFloorIndex)
    {
        m_itemDesc_Text.text = string.Format("해당 구역은 \n\n{0}층 부터 {1}층까지 \n\n이루어져 있어 \n\n몇 층을 공략 할래?", tStartFloorIndex, tEndFloorIndex);
    
    }

    public void ExitDungeonDeskText()
    {
        m_shopDictionary[ShopInfo.EntryDungeonButton].SetActive(false);
        m_itemDesc_Text.text = string.Format("던전 츄라이 츄라이");
    }

    public void ShowDungeonInfo(int index)
    {
        int tempIndex = index - 1;
        
        m_itemDesc_Text.text = string.Format("제 {0} 층을 공략 할래? \n\n\n\n\n\n\n\n\n\n<color=red>{1} RANK </color>부터 \n입장 가능 ", CDungeonData.GetInstance.m_dungeonList[tempIndex].m_floor , CDungeonData.GetInstance.m_dungeonList[tempIndex].m_level);

        
        CDungeonManager.GetInstance.m_floorIndex = tempIndex;
        m_curDungeonFloorIndex = tempIndex;

        //TODO : 추후 claer부분 유저 data의 clear로 변경
        //m_itemDesc_Text.text = string.Format("제 {0} 층을 공략 할래? \n\n 클리어 여부 : {1}", CDungeonData.GetInstance.m_dungeonList[index].m_floor, (CDungeonData.GetInstance.m_dungeonList[index].m_clear == 1) ? "Yes" : "No" );
        //버튼부분 유저 data의 clear체크후 참이면 활성화 아니면 비활성화 
        m_shopDictionary[ShopInfo.EntryDungeonButton].SetActive(true);
    }

    public void EntryDungeonAndChangeScene(string sceneName)
    {
        //TODO : 씬매니저에 다시 구현 
    }
}
