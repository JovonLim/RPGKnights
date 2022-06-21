using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Buffs : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buffText;
    [SerializeField] protected GameObject objectiveBox;
    protected enum Buff
    {
        health, attack, meleeDef, magicDef, atkspeed, def,
    }

    protected Buff buff;
    private bool chosen;
    protected bool getBuff;
    private string StartingText = "You gained";
    private string BackText = "for clearing the room";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!chosen)
        {
            buff = (Buff) Random.Range(0, 5);
            chosen = true;
        }
        
        if (getBuff)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            string FinalText;
            switch (buff)
            {
                case Buff.health:
                    {
                        player.GetComponent<PlayerHealth>().AddHealth(0.25f);
                        FinalText = StartingText + " 0.25 Health " + BackText;
                        buffText.text = FinalText;
                    }
                    break;
                case Buff.meleeDef:
                    {
                        player.GetComponent<PlayerHealth>().AddPhysicalDefense(1f);
                        FinalText = StartingText + " 1 PhysicalDefense " + BackText;
                        buffText.text = FinalText;
                    }
                    break;
                case Buff.magicDef:
                    {
                        player.GetComponent<PlayerHealth>().AddMagicDefense(1f);
                        FinalText = StartingText + " 1 MagicDefense " + BackText;
                        buffText.text = FinalText;
                    }
                    break;
                case Buff.def:
                    {
                        player.GetComponent<PlayerHealth>().AddDefense(1f);
                        FinalText = StartingText + " 1 Defense " + BackText;
                        buffText.text = FinalText;
                    }
                    break;
                case Buff.attack:
                    {
                        player.GetComponent<PlayerAttack>().AddAttack(0.2f);
                        FinalText = StartingText + " 0.2 Attack " + BackText;
                        buffText.text = FinalText;
                    }
                    break;
                case Buff.atkspeed:
                    {
                        player.GetComponent<PlayerAttack>().AddAttackSpeed(0.1f);
                        FinalText = StartingText + " 0.1 atkSpeed " + BackText;
                        buffText.text = FinalText;
                    }
                    break;        
            }

            StartCoroutine(Display());
            getBuff = false;
            

        }
    }

    IEnumerator Display()
    {
        yield return new WaitForSeconds(3);
        objectiveBox.SetActive(true);
        buffText.enabled = true;
        yield return new WaitForSeconds(2);
        objectiveBox.SetActive(false);
        buffText.enabled = false;
    }
}
