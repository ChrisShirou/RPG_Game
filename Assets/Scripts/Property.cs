using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour
{
    [SerializeField]
    [Header("屬性")]
    public int Lv;     //等級
    public float Exp;  //經驗值
    public float Hp;   //血量
    public float Mp;   //魔量
    public float Atk;  //攻擊力
    public float Matk; //魔法攻擊力
    public float Str;  //力量
    public float Int;  //智慧
    public float Con;  //體質
    public float Spi;  //精神
    public float Agi;  //敏捷

    public float Avd;  //迴避率
    public float Hrt;  //命中率
    public float Bkd;  //物理破防
    public float Pnt;  //魔法穿透
    public float Cti;  //爆擊
    public float Ctd;  //爆擊傷害
    public float Hlb;  //治療加成

    public float Def;  //物理防禦
    public float Mdef; //魔法防禦
    public float Ctid; //爆擊抗性
}
