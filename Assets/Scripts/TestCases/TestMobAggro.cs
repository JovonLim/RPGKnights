using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;


public class TestMobAggro
{
    private GameObject bandit;
    private GameObject FireWorm1;
    private GameObject FireWorm2;
    private Vector3 initialBanditPos;
    private Vector3 initialWorm1Pos;
    private Vector3 initialWorm2Pos;

    [SetUp] 
    public void Setup()
    {
        SceneManager.LoadScene("TestScene");
    }

    private void InitializeBandit()
    {
        bandit = GameObject.FindGameObjectWithTag("Enemy");
        initialBanditPos = bandit.transform.position;
        bandit.GetComponent<Aggro>().enabled = true;
    }

    private void InitializeFireWorm1()
    {
        FireWorm1 = GameObject.FindGameObjectWithTag("MiniBoss");
        initialWorm1Pos = FireWorm1.transform.position;
        FireWorm1.GetComponent<Aggro>().enabled = true;
    }

    private void InitializeFireWorm2()
    {
        FireWorm2 = GameObject.FindGameObjectWithTag("Boss");
        initialWorm2Pos = FireWorm2.transform.position;
        FireWorm2.GetComponent<Aggro>().enabled = true;
    }

    // Bandit moves towards player to melee attack
    [UnityTest]
    public IEnumerator TestBanditAggro()
    {
        InitializeBandit(); 
        yield return new WaitForSeconds(2);
        Assert.AreNotEqual(initialBanditPos, bandit.transform.position);
    }

    // First worm is in range to hit player, thus does not move
    [UnityTest]
    public IEnumerator TestFireWorm1DoesNotAggro()
    {
        InitializeFireWorm1();
        yield return new WaitForSeconds(2);
        Assert.AreEqual(initialWorm1Pos, FireWorm1.transform.position);
    }

    // Second worm is not in range to hit player, thus move forwards
    [UnityTest]
    public IEnumerator TestFireWorm2DoesAggro()
    {
        InitializeFireWorm2();
        yield return new WaitForSeconds(2);
        Assert.AreNotEqual(initialWorm2Pos, FireWorm2.transform.position);
    }


}
