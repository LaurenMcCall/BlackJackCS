using System;
using System.Collections.Generic;

namespace BlackJackCS
{
    public class Card
    {
        // properties of class Card: suit, rank, and value
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int Value { get; set; }

        override public string ToString()
        {
            return $"The {Rank} of {Suit}";
        }
    }

    public class Hand
    {
        // - Properties: a list of individual cards
        public List<Card> CurrentCards { get; set; }

        public Hand()
        {
            CurrentCards = new List<Card>();
        }
        // - Behaviors:
        //     - TotalValue representing the sum of the individual Cards in list.
        //         - Start with a total = 0
        //         - for each card in the hand do this:
        //             - add the amount of that card's value to total
        //         - return "total" as the result
        //     - add a card to the hand
        public void AddCard(Card cardToAdd)
        {
            // adds the supplied cad to the list of cards -- to the list of cards the Hand has
            CurrentCards.Add(cardToAdd);
        }
    }

    class Program
    {
        static void Main(string[] args)
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

            Console.WriteLine(player.CurrentCards.Count);
            Console.WriteLine(dealer.CurrentCards.Count);


            // - deal two cards to player face up
            // - deal two cards to dealer face down
            // - player's turn (method or class?)
            //   - player can hit or stand
            //   - if player hits
            //     - deal additional card
            //       - if hand total > 21, player busts
            //       - if player stands, then move on to dealer's turn
            //       - if player hits again, repeat line 43-45
            // - dealer's turn 
            //   - if dealer's hand > || = 17, dealer stands
            //   - if dealer's hand is < 17, dealer hits
            //   - repeat line 48-49
            //   - if dealer's hand is > 21, dealer busts
            // - display winner
            //   - winner has hand that is < || = to 21 and is closer to 21 than the other 
            //   - if both players have same hand totals (tie), dealer wins
            // - ask if player wants to play again
            //   - if yes, return to line 36
            //   - if no, exit the game

        }
    }

}

