using System.Collections.Generic;
using PCRCaculator.Guild;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
namespace PCRCaculator
{
    public class ExtraEffecDetail : MonoBehaviour
    {
        public Image Icon;
        public Text nameText;
        public Text describeText;
        public void Exit()
        {
            Destroy(gameObject);
        }
    }
}