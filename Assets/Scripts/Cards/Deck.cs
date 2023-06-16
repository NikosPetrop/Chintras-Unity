using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Deck : MonoBehaviour {
    public static Deck instance;
    
    [SerializeField] private CardPrefabs cardPrefabs;
    
    private List<BaseResourceCard> cards = new List<BaseResourceCard>(30);

    private void Awake() {
        instance = this;
    }

    public void AddCard(BaseResourceCard.ResourceType resourceType, int durability) {
        BaseResourceCard card;
        switch (resourceType) {
            case BaseResourceCard.ResourceType.Rock:
                card = Instantiate(cardPrefabs.ResourcesPrefabs.RockCardPrefab, PlayerHand.instance.transform);
                break;
            case BaseResourceCard.ResourceType.Wood:
                card = Instantiate(cardPrefabs.ResourcesPrefabs.WoodCardPrefab, PlayerHand.instance.transform);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
        }
        PlayerHand.instance.RepositionCards();
        
        card.Initialize(durability);
        card.DrawAnimation(new Vector3(0, 0, 0));
    }
    
    public BaseResourceCard DrawCard() {
        if (cards.Count == 0) {
            Debug.LogWarning("Deck is empty!");
            return null;
        }

        var drawnCard = cards[0];
        cards.RemoveAt(0);

        return drawnCard;
    }

    private void Shuffle() {
        int n = cards.Count;
        while (n > 1) {
            n--;
            int k = Random.Range(0, n + 1);
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }
    }
    
    [Serializable]
    private class CardPrefabs {
        public Resources ResourcesPrefabs;
        
        [Serializable]
        public class Resources {
            public RockCard RockCardPrefab;
            public WoodCard WoodCardPrefab;
        }
    }
}
