using UnityEngine;
using System.Collections.Generic;

namespace Code.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "AllAbilities", menuName = "SkillsData/ALL")]
    public class AbilitiesData : ScriptableObject
    {
       public  List<SkillSo> skills;
    }
}