using System;
using System.Collections.Generic;

namespace BlackJackCS
{
    public class Card
    {
        // properties of class Card: suit, rank, and value
        public string Suit { get; set; }
        public string Rank { get; set; }

        public int Value()
        {
            switch (Rank)
            {
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "10":
                    return int.Parse(Rank);

                case "Jack":
                case "Queen":
                case "King":
                    return 10;

                case "Ace":
                    return 11;

                default:
                    return 0;

            }

        }

        override public string ToString()
        {
            return $"The {Rank} of {Suit}";
        }
    }

    public class Hand
    {
        // - Properties: a list of individual cards
        public List<Card> CurrentCards { get; set; }

        // method withing class Hand
        public Hand()
        {
            CurrentCards = new List<Card>();
        }
        // - Behaviors:
        //     - TotalValue representing the sum of the individual Cards in the list (Hand).
        public int TotalValue()
        {
            //         - Start with a total = 0
            var total = 0;
            //         - for each card in the hand do this:
            foreach (var card in CurrentCards)
            {
                //   - add the amount of that card's value to total
                total = total + card.Value();
            }

            //         - return "total" as the result
            return total;
        }



        //     - add a card to the hand
        public void AddCard(Card cardToAdd)
        {
            // adds the supplied cad to the list of cards -- to the list of cards the Hand has
            CurrentCards.Add(cardToAdd);
        }
        public void PrintCardsAndTotal(string handName)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"{handName}, your cards are: ");
            Console.WriteLine("*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.");
            Console.WriteLine(String.Join(", and ", CurrentCards));
            Console.WriteLine("*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.");

            //      and the TotalValue of their Hand
            Console.WriteLine($"The total value of your hand is: {TotalValue()}");
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                // - create a blank list of cards -- call this `deck`
                var deck = new List<Card>();

                // - Make a list of the fours suits -- call this `suits`
                var suits = new List<string>() { "clubs", "Diamonds", "Hearts", "Spades" };

                // - Make of a list of 13 ranks -- call this list `ranks`
                var ranks = new List<string>() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

                // - Make a loop that goes through all the suits
                foreach (var suit in suits)
                {
                    //   Make a loop that goes through all the 'ranks'
                    foreach (var rank in ranks)
                    {
                        var card = new Card()
                        {
                            Suit = suit,
                            Rank = rank,
                        };

                        deck.Add(card);
                    }
                }

                // shuffle cards

                // numberOfCards = length of our deck
                var numberOfCards = deck.Count;
                // for rightIndex from numberOfCards - 1 down to 1 do:
                for (var rightIndex = numberOfCards - 1; rightIndex > 1; rightIndex--)
                {
                    //   leftIndex = random integer that is greater than or equal to 0 and LESS than rightIndex. See the section "How do we get a random integer")
                    var randomNumberGenerator = new Random();
                    var leftIndex = randomNumberGenerator.Next(rightIndex);

                    //   Now swap the values at rightIndex and leftIndex by doing this:
                    //     leftCard = the value from deck[leftIndex]
                    var leftCard = deck[leftIndex];
                    //     rightCard = the value from deck[rightIndex]
                    var rightCard = deck[rightIndex];
                    //     deck[rightIndex] = leftCard
                    deck[rightIndex] = leftCard;
                    //     deck[leftIndex] = rightCard
                    deck[leftIndex] = rightCard;
                }
                // Console.WriteLine(deck.Count);

                // VIEW ALL 52 SHUFFLED CARDS
                // foreach (var card in deck)
                // {
                //     Console.WriteLine(card);
                // }
                // - assign values to cards 

                //   - 2 through 9 = a value of that number
                //   - face cards = 10
                //   - ace = 11

                // create a player hand
                var player = new Hand();

                // create a dealer hand
                var dealer = new Hand();

                // ask the deck for card and deal to player 
                // - the card is equal to the 0th index of the deck list
                for (var numberOfCardsToDeal = 0; numberOfCardsToDeal < 2; numberOfCardsToDeal++)
                {
                    var card = deck[0];
                    // - remove that card from the deck list
                    deck.Remove(card);
                    // - call the "add card" behavior of the hand and pass it this card   
                    player.AddCard(card);
                }

                // ask the deck for card and deal to dealer 
                // - the card is equal to the 0th index of the deck list
                for (var numberOfCardsToDeal = 0; numberOfCardsToDeal < 2; numberOfCardsToDeal++)
                {
                    var card = deck[0];
                    // - remove that card from the deck list
                    deck.Remove(card);
                    // - call the "add card" behavior of the hand and pass it this card   
                    dealer.AddCard(card);
                }


                // 10. If they have BUSTED (hand TotalValue is > 21), then goto step 15
                var answer = "";

                while (player.TotalValue() < 21 && answer != "S")
                {
                    // 9.  Show the player the cards in their hand and the TotalValue of their Hand
                    //     Loop through the list of cards in player's hand
                    //         for every card, print out to the user the description of the card

                    player.PrintCardsAndTotal("Player");
                    // 11. Ask the player if they want to HIT or STAND
                    Console.Write("Do you want to (H)it or (S)tand? ");
                    answer = Console.ReadLine().ToUpper();
                    // 12. If HIT
                    if (answer == "H")
                    {
                        //  - Ask the deck for a card and place it in the player hand, repeat step 10

                        var newCard = deck[0];
                        deck.Remove(newCard);

                        player.AddCard(newCard);
                    }
                    // 13. If STAND then continue on
                }

                player.PrintCardsAndTotal("Player");

                // 14. If the dealer's hand TotalValue is more than 21 then goto step 17
                // 15. If the dealer's hand TotalValue is less than 17
                while (player.TotalValue() <= 21 && dealer.TotalValue() <= 17)
                {
                    //     - Add a card to the dealer hand and go back to 14
                    var newCard = deck[0];
                    deck.Remove(newCard);

                    dealer.AddCard(newCard);
                }

                // 16. Show the dealer's hand TotalValue
                dealer.PrintCardsAndTotal("Dealer");

                // 17. If the player's hand TotalValue > 21 show "DEALER WINS" 
                if (player.TotalValue() > 21)
                {
                    Console.WriteLine("DEALER WINS! ");
                }
                else
                // 18. If the dealer's hand TotalValue > 21 show "PLAYER WINS"

                if (dealer.TotalValue() > 21)
                {
                    Console.WriteLine("PLAYER WINS! ");
                }
                else
                // 19. If the dealer's hand TotalValue is more than the player's hand TotalValue then show "DEALER WINS", else show "PLAYER WINS"

                if (dealer.TotalValue() > player.TotalValue())
                {
                    Console.WriteLine("DEALER WINS! ");
                }
                else
                if (player.TotalValue() > dealer.TotalValue())
                {
                    Console.WriteLine("PLAYER WINS! ");
                }
                else
                {
                    // 20. If the value of the hands are even, show "DEALER WINS"
                    Console.WriteLine("DEALER WINS! ");
                }
            }
        }
    }

}

