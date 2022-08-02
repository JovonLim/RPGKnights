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
        player.AddComponent<CoinCounter>();
        CoinCounter.coins = 0;
        questInteraction.SetTestValues(kills, chests, rooms);
        PlayerQuestInteraction.AllocateChestCoins();
        PlayerQuestInteraction.AllocateKillCoins();
        PlayerQuestInteraction.AllocateRoomCoins();
    }

    // Test whether coins are given correctly per milestone
    [Test]
    public void TestPlayerMilestonesWithCoinGain()
    {
        InitializeQuestInteraction(5, 5, 3);
        Assert.AreEqual(260, CoinCounter.coins);
    }

    // Test that no coins are given when none of the milestones are hit
    [Test]
    public void TestPlayerMilestonesWithoutCoinGain()
    {
        InitializeQuestInteraction(2, 1, 2);
        Assert.AreEqual(0, CoinCounter.coins);
    }
}
