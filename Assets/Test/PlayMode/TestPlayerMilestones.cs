using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestPlayerMilestones
{
    // Initialize the milestone counts and resetting player coin amt to 0 for each test
    private void InitializeQuestInteraction(int kills, int chests, int rooms)
    {
        var player = new GameObject();
        PlayerQuestInteraction questInteraction = player.AddComponent<PlayerQuestInteraction>();
        player.AddComponent<UI>();
        UI.coins = 0;
        questInteraction.SetTestValues(kills, chests, rooms);
    }

    // Test whether coins are given correctly per milestone
    [UnityTest]
    public IEnumerator TestPlayerMilestonesWithCoinGainEnumeratorPasses()
    {
        InitializeQuestInteraction(10, 5, 6);
        yield return new WaitForSeconds(1);
        Assert.AreEqual(440, UI.coins);
    }

    // Test that no coins are given when none of the milestones are hit
    [UnityTest]
    public IEnumerator TestPlayerMilestonesWithoutCoinGainEnumeratorPasses()
    {
        InitializeQuestInteraction(2, 1, 2);
        yield return new WaitForSeconds(1);
        Assert.AreEqual(0, UI.coins);
    }
}
