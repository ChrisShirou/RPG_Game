using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStatus : MonoBehaviour
{
    [SerializeField]
    [Header("詠唱時間")]
    public float CastTime;
    [Header("施放時間")]
    public float SkillTime;
    [Header("連招時間")]
    public float ComboTime;
    [Header("冷卻時間")]
    public float ColdDownTime;
    [Header("攻擊倍率")]
    public float AttackMultiplier;

}
