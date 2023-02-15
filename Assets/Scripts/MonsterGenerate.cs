using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerate : MonsterList
{
    public float BornSpeed = 10f;

    public int slimeNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MonsterBorn",0f , BornSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MonsterBorn()
    {
        for (int i = slimeNumber; i < silmeMonster.slimeMaxNumber; i++)
        {
            slimeNumber++;
            Vector3 randomPos = new Vector3(silmeMonster.slimeBornPos.position.x + Random.Range(-5f, 5f),
                                            silmeMonster.slimeBornPos.position.y + 0.5f,
                                            silmeMonster.slimeBornPos.position.z + Random.Range(-5f, 5f));
            Instantiate(silmeMonster.slime, randomPos, transform.rotation, silmeMonster.slimeBornPos);
        }
    }
}
