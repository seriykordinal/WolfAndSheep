using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum SkillsId
{
    Bite, 
    Dash, 
    Preasure, 
    Howl
}

public class PlayerSkills : MonoBehaviour
{
    Dictionary<SkillsId, ISkill> skills;

    private void Start()
    {
        skills = new Dictionary<SkillsId, ISkill>()
        {
            {SkillsId.Bite, GetComponentInChildren<BiteSkill>() },
            {SkillsId.Dash, GetComponentInChildren<DashSkill>() },
            {SkillsId.Preasure, GetComponentInChildren<PreasureSkill>() },
            {SkillsId.Howl, GetComponentInChildren<HowlSkill>() }
        };
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            skills[SkillsId.Bite].UseSkill();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            skills[SkillsId.Dash].UseSkill();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            skills[SkillsId.Preasure].UseSkill();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            skills[SkillsId.Howl].UseSkill();
        }
    }

}
