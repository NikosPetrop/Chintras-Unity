using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {
    public static PlayerHand instance;

    [SerializeField] private float cardSpacing = 0.5f;
    [SerializeField] private float tiltAngle = 3.5f;
    [SerializeField] private float heightOffset = 20f;

    public int NumberOfCards;
    
    private float cardWidth = float.NaN;

    private void Start() {
        instance = this;
    }

    private void Update() {
        if (float.IsNaN(cardWidth)) {
            if (transform.childCount > 0) {
                var cardRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
                cardWidth = cardRectTransform.rect.width * 2 / 3;
            }
        }
        
        if (NumberOfCards != transform.childCount) {
            NumberOfCards = transform.childCount;
            RepositionCards();
        }
    }

    private void RepositionCards() {
        var totalWidth = ((cardWidth + cardSpacing) * (NumberOfCards - 1));
        var startPositionX = -totalWidth / 2f;

        var totalCurveAngle = tiltAngle * (NumberOfCards - 1);
        var startAngle = -totalCurveAngle / 2f;

        var centerOffset = (NumberOfCards % 2 == 0) ? cardWidth / 2f + cardSpacing / 2f : 0f;

        for (int i = 0; i < NumberOfCards; i++) {
            var card = transform.GetChild(i).GetComponent<BaseResourceCard>();

            var t = (float)i / (NumberOfCards - 1);
            var angle = Mathf.Lerp(startAngle, -startAngle, t);
            var xOffset = Mathf.Sin(Mathf.Deg2Rad * angle) * (cardWidth / 2f);
            var zOffset = Mathf.Cos(Mathf.Deg2Rad * angle) * (cardWidth / 2f);
            var yOffset = Mathf.Abs(i - (NumberOfCards - 1) / 2f) * -heightOffset;

            var newPositionX = startPositionX + (i * (cardWidth + cardSpacing)) + xOffset + centerOffset;
            Vector3 newPosition = new Vector3(newPositionX, yOffset, zOffset);

            card.Transform.localPosition = newPosition;
            card.Transform.localRotation = Quaternion.Euler(0f, 0f, -angle);
            card.Initialize();
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
