﻿using System;
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

    public class Deck
    {
        public List<Card> Cards { get; set; } = new List<Card>();
        // BEHAVIORS:
        // initialize a list of 52 cards
        public void Initialize()
        {
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

                    Cards.Add(card);
                }
            }
        }

        // shuffle
        public void Shuffle()
        {
            // numberOfCards = length of our deck
            var numberOfCards = Cards.Count;
            // for rightIndex from numberOfCards - 1 down to 1 do:
            for (var rightIndex = numberOfCards - 1; rightIndex > 1; rightIndex--)
            {
                //   leftIndex = random integer that is greater than or equal to 0 and LESS than rightIndex. See the section "How do we get a random integer")
                var randomNumberGenerator = new Random();
                var leftIndex = randomNumberGenerator.Next(rightIndex);
                //   Now swap the values at rightIndex and leftIndex by doing this:
                //     leftCard = the value from Cards[leftIndex]
                var leftCard = Cards[leftIndex];
                //     rightCard = the value from Cards[rightIndex]
                var rightCard = Cards[rightIndex];
                //     Cards[rightIndex] = leftCard
                Cards[rightIndex] = leftCard;
                //     Cards[leftIndex] = rightCard
                Cards[leftIndex] = rightCard;
            }
        }

        // deal
        public Card Deal()
        {
            var card = Cards[0];
            // - remove that card from the deck list
            Cards.Remove(card);

            return card;
        }

        public List<Card> DealMultiple(int numberOfCardsToDeal)
        {
            var multipleCards = new List<Card>();

            for (int count = 0; count < numberOfCardsToDeal; count++)
            {
                Card dealtCard = Deal();

                multipleCards.Add(dealtCard);
            }

            return multipleCards;
        }
    }

    public class Hand
    {
        // Properties: a list of individual cards
        public List<Card> CurrentCards { get; set; }

        // Constructor Method within class Hand
        public Hand()
        {
            CurrentCards = new List<Card>();
        }

        // Method: represents sum of Cards in the list (Hand)
        public int TotalValue()
        {
            // Start with a total of zero
            var total = 0;

            foreach (var card in CurrentCards)
            {
                // add the amount of that card's value to total
                total = total + card.Value();
            }
            //  return "total" as the result
            return total;
        }

        // Method: add a card to the hand
        public void AddCard(Card cardToAdd)
        {
            CurrentCards.Add(cardToAdd);
        }

        // Adds multiple cards to my hand
        public void AddCards(List<Card> cardsToAdd)
        {
            foreach (Card card in cardsToAdd)
            {
                AddCard(card);
            }
        }
        // Method: displays cards to player and dealer in console
        public void PrintCardsAndTotal(string handName)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{handName}, your cards are: ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(String.Join(", and ", CurrentCards));
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.*.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"The total value of your hand is: {TotalValue()}");
            Console.WriteLine();

        }

        public bool NotBusted()
        {
            return !Busted();
        }

        public bool Busted()
        {
            return TotalValue() > 21;

            // if (TotalValue() > 21)
            // {
            //     return true;
            // }
            // else
            // {
            //     return false;
            // }
        }
        public bool DealerShouldHit()
        {
            return TotalValue() <= 17;
            // if (TotalValue() <= 17)
            // {
            //     return true;
            // }
            // else
            // {
            //     return false;
            // }
        }
    }

    class Program
    {
        static void PlayTheGame()
        {
            // - create a blank list of cards -- call this `deck`
            var deck = new Deck();

            deck.Initialize();

            deck.Shuffle();

            deck.Deal();

            // create a player hand
            var player = new Hand();

            // create a dealer hand
            var dealer = new Hand();

            // ask the deck for card and deal to player 
            // - the card is equal to the 0th index of the deck list
            player.AddCards(deck.DealMultiple(2));

            dealer.AddCards(deck.DealMultiple(2));

            // ask the deck for card and deal to dealer 
            // - the card is equal to the 0th index of the deck list
            for (var numberOfCardsToDeal = 0; numberOfCardsToDeal < 2; numberOfCardsToDeal++)
            {
                Card card = deck.Deal();
                // - call the "add card" behavior of the hand and pass it this card   
                dealer.AddCard(card);
            }
            // Console.WriteLine(player.CurrentCards.ToString());
            // 10. If they have BUSTED (hand TotalValue is > 21), then goto step 15
            var answer = "";

            while (player.TotalValue() < 21 && answer != "S")
            {
                // 9.  Show the player the cards in their hand and the TotalValue of their Hand
                //     Loop through the list of cards in player's hand
                //         for every card, print out to the user the description of the card
                player.PrintCardsAndTotal("Player");
                // 11. Ask the player if they want to HIT or STAND
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Do you want to (H)it or (S)tand? ");
                answer = Console.ReadLine().ToUpper();
                // 12. If HIT
                if (answer == "H")
                {
                    Card card = deck.Deal();
                    player.AddCard(card);
                }
                // 13. If STAND then continue on
            }

            player.PrintCardsAndTotal("Player");
            // 14. If the dealer's hand TotalValue is more than 21 then goto step 17
            // 15. If the dealer's hand TotalValue is less than 17
            while (player.NotBusted() && dealer.DealerShouldHit())
            {
                //     - Add a card to the dealer hand and go back to 14
                Card card = deck.Deal();
                dealer.AddCard(card);
            }

            // 16. Show the dealer's hand TotalValue
            dealer.PrintCardsAndTotal("Dealer");

            // 17. If the player's hand TotalValue > 21 show "DEALER WINS" 
            if (player.Busted())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("DEALER WINS! 👊 ");
            }
            else
            // 18. If the dealer's hand TotalValue > 21 show "PLAYER WINS"
            if (dealer.Busted())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("PLAYER WINS! 🥳 ");
            }
            else
            // 19. If the dealer's hand TotalValue is more than the player's hand TotalValue then show "DEALER WINS", else show "PLAYER WINS"
            if (dealer.TotalValue() > player.TotalValue())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("DEALER WINS! 👊 ");
            }
            else
            if (player.TotalValue() > dealer.TotalValue())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("PLAYER WINS! 🥳 ");
            }
            else
            {
                // 20. If the value of the hands are even, show "DEALER WINS"
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("DEALER WINS! 👊 ");
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                PlayTheGame();

                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Would you like to play again? (Y)es or (N)o ");
                var answer = Console.ReadLine().ToUpper();

                if (answer == "N")
                {
                    break;
                }
            }
        }
    }

}

