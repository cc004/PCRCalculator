using System.Collections;
using Elements;
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
            while (castTime >= 0)
            {
                if (transform == null)
                {
                    yield break; 
                }
                //Vector3 pos = posFix + 18 * transform.position;
                Vector3 pos = posFix + Camera.main.WorldToScreenPoint(transform.position);
                pos.z = 0;
                this.transform.position = pos;
                
                    castTime -= BattleHeaderController.Instance.IsPaused ? 0 : Time.deltaTime;
                
                yield return null;
            }
            if (gameObject != null)
                Destroy(gameObject);
        }
    }
}