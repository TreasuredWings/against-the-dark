using System.Collections.Generic;
using UnityEngine;

public class UpgradeDeck
{
    public List<Upgrade> Deck {get; private set;} = new List<Upgrade>();

    public UpgradeDeck(List<Upgrade> newDeck)
    {
        Deck = newDeck;
    }

    // Shuffle the list
    public void Shuffle()
    {
        int count = Deck.Count;
		int last = count - 1;
		for (int i = 0; i < last; ++i) {
			int r = Random.Range(i, count);
			Upgrade temp = Deck[i];
			Deck[i] = Deck[r];
			Deck[r] = temp;
		}
    }    

    // Draw the first element of the list
    public Upgrade Draw()
    {
        Upgrade temp = Deck[0];
        Deck.RemoveAt(0);
        return temp;
    }

    // Add an element to the back of the list
    public void Return(Upgrade toAdd)
    {
        Deck.Add(toAdd);
    }

    // Set the deck to a new list of upgrades
    public void ReplaceDeck(List<Upgrade> replacement)
    {
        Deck = replacement;
    }
}
