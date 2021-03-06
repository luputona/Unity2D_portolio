﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
//마을에서만 이용  
// 스텟찍을때만 갱신 하는 클래스 
public class CStatus : SingleTon<CStatus>
{
    private const double LimitedDodge = 75;

    public Text text;

    public enum ESTATUS
    {
        Damage,
        Defence,
        Dodge,
        Hp,
        Str,
        Dex,
        Default = 99
    };
    
    public ESTATUS m_eStatus = ESTATUS.Default;

    
    public string m_id;

    public double m_defDamage;
    public double m_defDefence;
    public double m_defDodge;   
    public double m_defHp;
    public double m_defStr;
    public double m_defDex;
    public double m_weaponDamage;
    public double m_weaponDodge;
    public double m_weaponDef;
    public double m_exceedDodge;
    public double m_weaponHp;

    public double m_curhp;
    public double m_curDamage;
    public double m_curDefence;
    public double m_curDodge;
    public double m_curHp;
    public double m_curStr;
    public double m_curDex;

    public string ID
    {        
        get
        {
            return m_id;
        }     
    }
    //public double DefDamage { get { return m_defDamage; } }
    //public double DefDefence { get { return m_defDefence; } }
    //public double DefDodge { get { return m_defDodge; } }
    //public double DefStr { get { return m_defStr; } }
    //public double DefDex { get { return m_defDex; } }

    public double DefHp
    {
        get
        {
            return m_defHp;
        }        
    }


    public double SetWeaponDamage
    {
        set
        {
            m_weaponDamage = value;
        }       
    }
    public double SetWeaponDef
    {
        set
        {
            m_weaponDef = value;
        }
    }
    
    public double SetWeaponDodge
    {
        set
        {
            m_weaponDodge = value;
        }
    }

    public double SetWeaponHP
    {
        set
        {
            m_weaponHp = value;
        }
    }


    //TODO :  디폴트값과 현재값 다시 제작
    void Start()
    {
        m_defDamage = CUserData.GetInstance.m_userStatusList[0].damage;
        m_defDefence = CUserData.GetInstance.m_userStatusList[0].def;
        m_defDodge = CUserData.GetInstance.m_userStatusList[0].dodge;
        m_defHp = CUserData.GetInstance.m_userStatusList[0].hp;
        m_defStr = CUserData.GetInstance.m_userStatusList[0].str;
        m_defDex = CUserData.GetInstance.m_userStatusList[0].dex;

        text.text = string.Format("{0},{1},{2}",m_defHp, m_defStr, m_defDex);
    }

    //DB에서 바로 받아오는 초기 셋팅값
    public void InitSetStatus(double damage, double def, double dodge, double hp, double str, double dex)
    {
        //공 방 회 피 힘 덱
        m_defDamage =  damage;
        m_defDefence =  def;
        m_defDodge =  dodge;
        m_defHp =  hp;
        m_defStr = str;
        m_defDex = dex;
      
    }

    public void RecordStatus( )
    {        
        if (ESTATUS.Hp == m_eStatus)
        {            
            if (CUpdateUserInfo.GetInstance.m_point >= 0)
            {
                m_defHp += 1;
                CUpdateUserInfo.GetInstance.m_point -= 1;
                //Debug.Log(CUpdateUserInfo.GetInstance.m_point);
            }
            else
            {
                return;
            }            
        }
        else if (ESTATUS.Str == m_eStatus)
        {            
            if (CUpdateUserInfo.GetInstance.m_point >= 0)
            {
                m_defStr += 1;
                CUpdateUserInfo.GetInstance.m_point -= 1;
                
            }
            else
            {
                return;
            }
        }
        else if (ESTATUS.Dex == m_eStatus)
        {            
            if (CUpdateUserInfo.GetInstance.m_point >= 0)
            {
                m_defDex += 1;
                CUpdateUserInfo.GetInstance.m_point -= 1;
                
            }
            else
            {
                return;
            }
        }

        CalculateStatus();
        CUpdateUserInfo.GetInstance.UpdateStatus();
        
        StartCoroutine(CUploadUserData.GetInstance.UploadUserStatus());
        
    
    }

    //TODO : 스테이터스 UI 가 true가 되면 작동되게 해야함
    public void CalculateStatus()
    {
        CalculateDamage();
        CalculateDef();
        CalculateDodge();
        CalculateHp();
    }

    void CalculateDamage()
    {
        double tAdd = (m_weaponDamage + m_defStr) * 0.01;
        m_defDamage = (m_weaponDamage * 0.1 + m_defStr * 0.1 * tAdd) + m_weaponDamage;      
    }

    void CalculateDef()
    {
        // 증감치 =  회피 초과분 * 0.01 + 회피초과분 * 0.04 
        // 방어 = 기존방어(장비방어) + 증감치       
        double tAdd = (m_exceedDodge * 0.01) + (m_exceedDodge * 0.04);
        m_defDefence = m_weaponDef + tAdd;

    }
    void CalculateDodge()
    {
        double tAdd = (m_weaponDodge * 0.8 * (m_defDex * 0.2)) + m_defDex * 0.01;
        double tdodge = m_weaponDodge + tAdd;

        //민첩이 75보다 높은지 체크
        
        m_defDodge = tdodge;
        if (tdodge > LimitedDodge)
        {
            m_defDodge = LimitedDodge;
            m_exceedDodge = tdodge - LimitedDodge;
            CalculateDef();
        }        
    }
    void CalculateHp()
    {
        double tHp = m_weaponHp + m_defHp;
        m_curhp = tHp;
    }
}
