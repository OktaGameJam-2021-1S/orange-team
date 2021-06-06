using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointsUpdator : MonoBehaviour
{
    [SerializeField] private Text labelHealth;
    [SerializeField] private Text labelStrength;
    [SerializeField] private Text labelLuck;
    [SerializeField] private Text labelSpeed;
    [SerializeField] private Text labelSkillpoints;

    // Start is called before the first frame update
    void Start()
    {
        labelHealth.text = GetLife().ToString();
        labelStrength.text = GetStrength().ToString();
        labelLuck.text = GetLuck().ToString();
        labelSpeed.text = GetSpeed().ToString();
        labelSkillpoints.text = GetSkillpoints().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region "Gets"

    private int GetSkillpoints()
    {
        return 25;
    }

    private int GetLife()
    {
        return 1;
    }

    private int GetStrength()
    {
        return 1;
    }
    private int GetLuck()
    {
        return 1;
    }
    private int GetSpeed()
    {
        return 1;
    }
    #endregion

    #region "Sets"

    public void SetLife(bool plus)
    {
        int finalValue = GetLife();
        if (plus) { finalValue = GetLife() + 1; SetSkillpoints(false); }
        else if (GetLife() !=0) { finalValue = GetLife() - 1; SetSkillpoints(true); }
        // life variable = finalValue
        labelHealth.text = finalValue.ToString();
    }
    public void SetStrength(bool plus)
    {
        int finalValue = GetStrength();
        if (plus) { finalValue = GetStrength() + 1; SetSkillpoints(false); }
        else if (GetStrength() != 0) { finalValue = GetStrength() - 1; SetSkillpoints(true); }
        // str variable = finalValue
        labelStrength.text = finalValue.ToString();
    }
    public void SetLuck(bool plus)
    {
        int finalValue = GetLuck();
        if (plus) { finalValue = GetLuck() + 1; SetSkillpoints(false); }
        else if (GetLuck() != 0) { finalValue = GetLuck() - 1; SetSkillpoints(true); }
        // luck variable = finalValue
        labelLuck.text = finalValue.ToString();
    }
    public void SetSpeed(bool plus)
    {
        int finalValue = GetSpeed();
        if (plus) { finalValue = GetSpeed() + 1; SetSkillpoints(false); }
        else if (GetSpeed() != 0) { finalValue = GetSpeed() - 1; SetSkillpoints(true); }
        // speed variable = finalValue
        labelSpeed.text = finalValue.ToString();
    }

    public void SetSkillpoints(bool plus)
    {
        int finalValue = GetSkillpoints();
        if (plus) { finalValue = GetSkillpoints() + 1; }
        else if (GetSkillpoints() != 0) { finalValue = GetSkillpoints() - 1; }
        // skillpoints variable = finalValue
        labelSkillpoints.text = "x " + finalValue.ToString();
    }

    #endregion
}
