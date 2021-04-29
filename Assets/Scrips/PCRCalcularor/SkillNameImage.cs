using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Battle
{


    public class SkillNameImage : MonoBehaviour
    {
        public Text nameText;
        public Vector3 posFix;
        public void SetName(string name, Transform transform)
        {
            nameText.text = name;
            StartCoroutine(FollowUnit(transform));
        }
        private IEnumerator FollowUnit(Transform transform)
        {
            float castTime = 1;
            bool k = BattleManager.Instance == null;
            while (castTime >= 0)
            {
                //Vector3 pos = posFix + 18 * transform.position;
                Vector3 pos = posFix + Camera.main.WorldToScreenPoint(transform.position);
                pos.z = 0;
                this.transform.position = pos;

                if (k)
                {
                    castTime -= Elements.BattleHeaderController.Instance.IsPaused ? 0 : Time.deltaTime;
                }
                else
                {
                    castTime -= BattleManager.Instance.DeltaTimeForPause;
                }
                
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}