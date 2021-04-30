using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft0.Json;
using System.IO;
using Mono.Data.Sqlite;


public class CreateSQLData
{
    static string SQLpath = "D:/PCRCalculator/redive_master_db_diff-master/";
    [MenuItem("PCRTools/用SQL导入数据/导入角色数据")]
    public static void ImportCharData()
    {
        string conn = "redive_jp.db";
        SQLiteHelper sql = new SQLiteHelper(conn);
        AddTable(sql, "chara_story_status");
        AddTable(sql, "equipment_data");
        AddTable(sql, "equipment_enhance_data");
        AddTable(sql, "equipment_enhance_rate");
        AddTable(sql, "skill_action");
        AddTable(sql, "skill_data");
        AddTable(sql, "story_detail");
        AddTable(sql, "unique_equipment_data");
        AddTable(sql, "unique_equipment_enhance_rate");
        AddTable(sql, "unique_equipment_rankup");
        AddTable(sql, "unit_attack_pattern");
        //AddTable(sql, "unit_data");
        AddTable(sql, "unit_promotion");
        AddTable(sql, "unit_promotion_status");
        AddTable(sql, "unit_rarity");
        AddTable(sql, "unit_skill_data");
        AddTable(sql, "unit_status_coefficient");


        sql.CloseConnection();
        Debug.Log("成功！");
    }
    private static void AddTable(SQLiteHelper sql, string tableName)
    {
        string sqlSTR = LoadSQLText(tableName);
        var table = sql.ExecuteQuery(sqlSTR);
    }
    private static string LoadSQLText(string name)
    {
        string filePath = SQLpath + name + ".sql";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string jsonStr = sr.ReadToEnd();
            sr.Close();
            return jsonStr;
        }
        Debug.LogError("sql数据" + name + "读取失败！");
        return "";

    }
}
