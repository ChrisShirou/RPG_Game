using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerManager : Property
{
    public Image HpBar;
    private Animator animator;
    private Transform Target;
    private float Max_Hp;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Max_Hp = this.Hp;
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 50; i >= -50; i -= 10) {
        //    CreateRayCast(transform.position, transform.forward * 100 + transform.right * i, Color.red);
        //}
        HpBar.rectTransform.sizeDelta = new Vector2(this.Hp / Max_Hp * 100, 10);
    }
    void CreateRayCast(Vector3 pos, Vector3 direction, Color color)
    {
        Ray ray = new Ray(pos, direction);

        Debug.DrawLine(pos, pos + direction, color);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            print(hit.point);
            print(hit.transform.position);
            print(hit.collider.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Target = other.transform;
        }
    }
    public void Move(Vector3 move_pos)
    {
        transform.position = transform.position + move_pos;
    }
    public void PlayerRotate(float value)
    {
        value = Mathf.Repeat(value, 360);
        transform.rotation = UnityEngine.Quaternion.Euler(0, value, 0);
    }
    public void PlayerAnimation(string anim_name)
    {
        animator.Play(anim_name);
    }
    public void PlayerSkillCheck() 
    {

    }
    public void PlayerAttack() 
    {
        if (Target)
        {
            print("攻擊範圍內有目標");
            float enemy_hp = Target.GetComponent<EnemyManager>().Hp;
            enemy_hp -= this.Atk;
            Target.GetComponent<EnemyManager>().Hp = enemy_hp;
        }
        else 
        {
            print("攻擊範圍內無目標");
        }
    }
}
