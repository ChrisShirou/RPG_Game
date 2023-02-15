using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SilmeMonster
{
    public GameObject slime;
    public Transform slimeBornPos;
    public int slimeMaxNumber;
}
    public class MonsterList : MonoBehaviour
{
    [Header("史萊姆")]
    public SilmeMonster silmeMonster;
}
