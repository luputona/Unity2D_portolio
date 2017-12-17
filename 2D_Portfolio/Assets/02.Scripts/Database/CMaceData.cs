﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System.Text;

public class CMaceData : SingleTon<CMaceData>, IItemData
{

    [SerializeField]
    private string m_serverUrl;
    [SerializeField]
    private JsonData m_maceJsonData;

    public List<SwordItem> m_maceItemList = new List<SwordItem>();

    public void Awake()
    {
        StartCoroutine(LoadData());
    }
    public void Start()
    {
    }

    public void ConstructData()
    {
        for (int i = 0; i < m_maceJsonData.Count; i++)
        {
            m_maceItemList.Add(new SwordItem(
                (int)m_maceJsonData[i]["id"],
                m_maceJsonData[i]["name"].ToString(),
                m_maceJsonData[i]["discription"].ToString(),
                m_maceJsonData[i]["skill_name"].ToString(),
                m_maceJsonData[i]["skill_Dis"].ToString(),
                double.Parse(m_maceJsonData[i]["skill_effect_01"].ToString()),
                double.Parse(m_maceJsonData[i]["skill_effect_02"].ToString()),
                double.Parse(m_maceJsonData[i]["skill_effect_03"].ToString()),
                double.Parse(m_maceJsonData[i]["skill_effect_04"].ToString()),
                double.Parse(m_maceJsonData[i]["damage"].ToString()),
                double.Parse(m_maceJsonData[i]["def"].ToString()),
                double.Parse(m_maceJsonData[i]["dodging"].ToString()),
                double.Parse(m_maceJsonData[i]["hp"].ToString()),
                (int)m_maceJsonData[i]["cost"]));
        }
    }

    public IEnumerator LoadData()
    {
        WWW www = new WWW(m_serverUrl);

        yield return www;

        //byte[] bytes = Encoding.Default.GetBytes(serverDB);
        string serverDB = Encoding.UTF8.GetString(www.bytes);

        m_maceJsonData = JsonMapper.ToObject(serverDB);

        if (www.isDone)
        {
            ConstructData();
        }
        

    }
    public void LoadLocalData()
    { }
    public void ConstructLocalData() { }
}


[System.Serializable]
public class MaceItem
{
    public int m_id;//{ get; set; }
    public string m_name;// { get; set; }
    public string m_discription;// { get; set; }
    public string m_skill_name;// { get; set; }
    public string m_skill_Dis;// { get; set; }
    public double m_skill_effect_01;// { get; set; }
    public double m_skill_effect_02;// { get; set; }
    public double m_skill_effect_03;// { get; set; }
    public double m_skill_effect_04;// { get; set; }
    public double m_damage;// { get; set; }
    public double m_def;// { get; set; }
    public double m_dodging;//{ get; set; }
    public double m_hp;//{ get; set; }
    public int m_cost;// { get; set; }

    public MaceItem(int id, string name, string discription, string skill_name, string skill_dis,
        double skill_effect_01, double skill_effect_02,
        double skill_effect_03, double skill_effect_04, double damage,
        double def, double dodging, double hp, int cost)
    {
        m_id = id;
        m_name = name;
        m_discription = discription;
        m_skill_name = skill_name;
        m_skill_Dis = skill_dis;
        m_skill_effect_01 = skill_effect_01;
        m_skill_effect_02 = skill_effect_02;
        m_skill_effect_03 = skill_effect_03;
        m_skill_effect_04 = skill_effect_04;
        m_damage = damage;
        m_def = def;
        m_dodging = dodging;
        m_hp = hp;
        m_cost = cost;
    }

}