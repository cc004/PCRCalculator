// using System;
// using System.Collections.Generic;
// using UnityEngine;

// namespace Elements
// {
//     [Serializable]
//     public class PrincessKnightInfo
//     {
//         // 属性
//         public List<TalentLevelInfo> TalentLevelInfoList;
//         public List<TalentSkillNodeInfo> TalentSkillLastEnhancedPageNodeList;
//         public TeamSkillNodeInfo TeamSkillLatestNode;

//         // 构造函数
//         public PrincessKnightInfo()
//         {
//             initializePrincessKnightInfo();
//         }

//         public PrincessKnightInfo(string jsonString)
//         {
//             initializePrincessKnightInfo();
//             ParsePrincessKnightInfo(jsonString);
//         }

//         // 解析方法
//         public void ParsePrincessKnightInfo(string jsonText)
//         {
//             JsonUtility.FromJsonOverwrite(jsonText, this);
//         }

//         // 设置方法
//         public void SetTalentLevelInfoList(List<TalentLevelInfo> _talentLevelInfoList)
//         {
//             TalentLevelInfoList = _talentLevelInfoList;
//         }
//         public void BuildFromPlayerSetting(Dictionary<int, int> talentLevelDic)
//         {
//             TalentLevelInfoList.Clear();
//             foreach (var pair in talentLevelDic)
//             {
//                 var info = new TalentLevelInfo();
//                 info.SetTalentId(pair.Key);
//                 info.SetTotalPoint(pair.Value * 20); 
//                 TalentLevelInfoList.Add(info);
//             }
//         }

//         public void SetTalentSkillLastEnhancedPageNodeList(List<TalentSkillNodeInfo> _talentSkillLastEnhancedPageNodeList)
//         {
//             TalentSkillLastEnhancedPageNodeList = _talentSkillLastEnhancedPageNodeList;
//         }

//         public void SetTeamSkillLatestNode(TeamSkillNodeInfo _teamSkillLatestNode)
//         {
//             TeamSkillLatestNode = _teamSkillLatestNode;
//         }

//         // 初始化方法
//         private void initializePrincessKnightInfo()
//         {
//             TalentLevelInfoList = new List<TalentLevelInfo>();
//             TalentSkillLastEnhancedPageNodeList = new List<TalentSkillNodeInfo>();
//             TeamSkillLatestNode = null;
//         }
//     }
// }