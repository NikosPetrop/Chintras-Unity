using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {
    public static PlayerHand instance;

    [SerializeField] private float cardSpacing = -50f;
    [SerializeField] private float tiltAngle = 3.5f;
    [SerializeField] private float heightOffset = 20f;
    
    private void Start() {
        instance = this;
    }

    public void RepositionCards() {
        const float cardWidth = 265f;
        var numberOfCards = transform.childCount;
        
        // Handle the case when there is only one card
        if (numberOfCards == 1) {
            var card = transform.GetChild(0).GetComponent<BaseResourceCard>();
            card.SetHandPositionAndRotation(Vector3.zero, Quaternion.identity);
            return;
        }
        
        var totalWidth = ((cardWidth + cardSpacing) * (numberOfCards - 1));
        var startPositionX = -totalWidth / 2f;

        var totalCurveAngle = tiltAngle * (numberOfCards - 1);
        var startAngle = -totalCurveAngle / 2f;

        var centerOffset = (numberOfCards % 2 == 0) ? cardWidth / 2f + cardSpacing / 2f : 0f;

        for (int i = 0; i < numberOfCards; i++) {
            
            var card = transform.GetChild(i).GetComponent<BaseResourceCard>();
            
            var t = (float)i / (numberOfCards - 1);
            var angle = Mathf.Lerp(startAngle, -startAngle, t);
            var xOffset = Mathf.Sin(Mathf.Deg2Rad * angle) * (cardWidth / 2f);
            var zOffset = Mathf.Cos(Mathf.Deg2Rad * angle) * (cardWidth / 2f);
            var yOffset = Mathf.Abs(i - (numberOfCards - 1) / 2f) * -heightOffset;

            var newPositionX = startPositionX + (i * (cardWidth + cardSpacing)) + xOffset + centerOffset;
            
            var newPosition = new Vector3(newPositionX, yOffset, zOffset);
            var newRotation = Quaternion.Euler(0f, 0f, -angle);
            card.SetHandPositionAndRotation(newPosition, newRotation);
        }
    }

    public List<BaseResourceCard> GetCards() {
        var cards = new List<BaseResourceCard>();
        for (int i = 0; i < transform.childCount; i++) {
            var card = transform.GetChild(i).GetComponent<BaseResourceCard>();
            cards.Add(card);
        }

        return cards;
    }
}
