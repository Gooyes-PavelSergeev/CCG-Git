# CCG-Git
 
 At start it's initialized with 7 cards
 Limit of cards in hand - 7
 
Use button PLUS to add card to your hand
Use button REFRESH to randomly change one random parameter of each card in hand

When health is dropped to 0 or lower, it will be destroyed
Actually, it's being destroyed as soon as refresh will drop it, but visualization will wait till tween is complete and then destroy it
It means that card becomes inactive before it's visually destroyed

Also I made it to detect if you click on card, but everything that happens is Debug.Log about click
When it detects that you point on card, the card will show itself (like in HearthStone)
