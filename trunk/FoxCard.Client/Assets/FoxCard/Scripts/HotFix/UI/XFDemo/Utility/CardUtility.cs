using System;
using System.Collections.Generic;
using System.Linq;

namespace HotFix_UI
{
    public class CardUtility
    {
        public static CardGroupType DetectCardGroupType(List<Card> cards)
        {
            
            if (IsFiveOfAKindFlush(cards))
            {
                return CardGroupType.FiveOfAKindFlush;
            }
            else if (IsFullHouseFlush(cards))
            {
                return CardGroupType.FullHouseFlush;
            }
            else if (IsFiveOfAKind(cards))
            {
                return CardGroupType.FiveOfAKind;
            }
            else if (IsRoyalFlush(cards))
            {
                return CardGroupType.RoyalFlush;
            }
            else if (IsStraightFlush(cards))
            {
                return CardGroupType.StraightFlush;
            }
            else if (IsFourOfAKind(cards))
            {
                return CardGroupType.FourOfAKind;
            }
            else if (IsFullHouse(cards))
            {
                return CardGroupType.FullHouse;
            }
            else if (IsFlush(cards))
            {
                return CardGroupType.Flush;
            }
            else if (IsStraight(cards))
            {
                return CardGroupType.Straight;
            }
            else if (IsThreeOfAKind(cards))
            {
                return CardGroupType.ThreeOfAKind;
            }
            else if (IsTwoPair(cards))
            {
                return CardGroupType.TwoPair;
            }
            else if (IsPair(cards))
            {
                return CardGroupType.OnePair;
            }
            else
            {
                return CardGroupType.HighCard;
            }
        }

        public static List<Card> GetEffectiveCards(CardGroupType type, List<Card> cards)
        {
            List<Card> outCards = new List<Card>();
            int straightInternal = PlayerSingleton.Instance.straightInternal;
            int flushSize = PlayerSingleton.Instance.flushSize;
            var sortedCards = cards.OrderBy(c => c.points).ToList();

            switch (type)
            {
                case CardGroupType.HighCard:
                    outCards.Add(sortedCards[sortedCards.Count - 1]);

                    break;
                case CardGroupType.OnePair:
                    outCards = cards.GroupBy(c => c.points)
                        .Where(g => g.Count() >= 2)
                        .SelectMany(g => g.Take(2))
                        .ToList();

                    break;
                case CardGroupType.TwoPair:
                    outCards = cards.GroupBy(c => c.points)
                        .Where(g => g.Count() >= 2)
                        .SelectMany(g => g.Take(2))
                        .ToList();
                    break;
                case CardGroupType.ThreeOfAKind:
                    outCards = cards.GroupBy(c => c.points) // 按点数分组
                        .Where(g => g.Count() >= 3) // 只保留具有三张或更多张牌的组
                        .SelectMany(g => g.Take(3)) // 从每个组中取出三张牌
                        .ToList(); // 转换为 List
                    break;
                case CardGroupType.Straight:
                    if (cards.Count == 5)
                    {
                        switch (flushSize)
                        {
                            case 4:


                                if ((int)sortedCards[1].points - (int)sortedCards[0].points > straightInternal)
                                {
                                    sortedCards.RemoveAt(0);
                                    outCards = sortedCards;
                                }
                                else if ((int)sortedCards[sortedCards.Count - 1].points -
                                         (int)sortedCards[sortedCards.Count - 2].points > straightInternal)
                                {
                                    sortedCards.RemoveAt(sortedCards.Count - 1);
                                    outCards = sortedCards;
                                }
                                else
                                {
                                    for (int i = 0; i < sortedCards.Count - 1; i++)
                                    {
                                        if ((int)sortedCards[i + 1].points == (int)sortedCards[i].points)
                                        {
                                            //TODO:
                                            if (sortedCards[i + 1].cardGain.enhance > sortedCards[i].cardGain.enhance)
                                            {
                                                sortedCards.RemoveAt(i);
                                            }
                                            else
                                            {
                                                sortedCards.RemoveAt(i + 1);
                                            }
                                        }
                                    }

                                    outCards = sortedCards;
                                }

                                break;
                            case 5:
                                outCards = cards;
                                break;
                        }
                    }
                    else
                    {
                        outCards = cards;
                    }

                    break;
                case CardGroupType.Flush:
                    if (cards.Count >= 5)
                    {
                        switch (flushSize)
                        {
                            case 4:
                                outCards = cards.GroupBy(c => c.color)
                                    .Where(g => g.Count() == 4)
                                    .SelectMany(g => g)
                                    .ToList();
                                break;
                            case 5:
                                outCards = cards;
                                break;
                        }
                    }
                    else
                    {
                        outCards = cards;
                    }


                    break;
                case CardGroupType.FullHouse:
                    break;
                case CardGroupType.FourOfAKind:
                    break;
                case CardGroupType.StraightFlush:
                    if (cards.Count == 5)
                    {
                        switch (flushSize)
                        {
                            case 4:
                                //有一张4张即可小丑牌 有可能有一张不计分 有可能全计分
                                var outCards1 = cards.GroupBy(c => c.color)
                                    .Where(g => g.Count() == 4)
                                    .SelectMany(g => g)
                                    .ToList();
                                if (outCards1.Count == 4)
                                {
                                    //4张都为同花
                                    outCards = outCards1;
                                }
                                else
                                {
                                    //5张都为同花

                                    if ((int)sortedCards[1].points - (int)sortedCards[0].points > straightInternal)
                                    {
                                        sortedCards.RemoveAt(1);
                                        outCards = sortedCards;
                                    }
                                    else if ((int)sortedCards[sortedCards.Count - 1].points -
                                             (int)sortedCards[sortedCards.Count - 2].points > straightInternal)
                                    {
                                        sortedCards.RemoveAt(sortedCards.Count - 1);
                                        outCards = sortedCards;
                                    }
                                    else
                                    {
                                        outCards = cards;
                                    }
                                }

                                break;
                            case 5:
                                outCards = cards;
                                break;
                        }
                    }
                    else
                    {
                        outCards = cards;
                    }


                    break;
                case CardGroupType.RoyalFlush:
                    if (cards.Count == 5)
                    {
                        switch (flushSize)
                        {
                            case 4:
                                //有一张4张即可小丑牌 有可能有一张不计分 有可能全计分
                                var outCards1 = cards.GroupBy(c => c.color)
                                    .Where(g => g.Count() == 4)
                                    .SelectMany(g => g)
                                    .ToList();
                                if (outCards1.Count == 4)
                                {
                                    //4张都为同花
                                    outCards = outCards1;
                                }
                                else
                                {
                                    //5张都为同花

                                    if ((int)sortedCards[1].points - (int)sortedCards[0].points > straightInternal)
                                    {
                                        sortedCards.RemoveAt(1);
                                        outCards = sortedCards;
                                    }
                                    else
                                    {
                                        outCards = cards;
                                    }
                                }

                                break;
                            case 5:
                                outCards = cards;
                                break;
                        }
                    }
                    else
                    {
                        outCards = cards;
                    }

                    break;
                case CardGroupType.FiveOfAKind:

                    break;
                case CardGroupType.FiveOfAKindFlush:
                    break;
                case CardGroupType.FullHouseFlush:
                    break;
            }

            outCards = outCards.OrderByDescending(c => c.points).ToList();
            return outCards;
        }

        #region JudgeEveryGroup

        private static bool IsPair(List<Card> cards)
        {
            var groups = cards.GroupBy(c => c.points);
            return groups.Any(group => group.Count() == 2);
        }

        private static bool IsTwoPair(List<Card> cards)
        {
            var groups = cards.GroupBy(c => c.points);
            return groups.Count(group => group.Count() == 2) == 2;
        }

        private static bool IsThreeOfAKind(List<Card> cards)
        {
            var groups = cards.GroupBy(c => c.points);
            return groups.Any(group => group.Count() == 3);
        }

        private static bool IsStraight(List<Card> cards)
        {
            int straightInternal = PlayerSingleton.Instance.straightInternal;
            int flushSize = PlayerSingleton.Instance.flushSize;
            if (cards.Count < flushSize)
            {
                return false;
            }

            var sortedCards = cards.OrderBy(c => c.points).ToList();

            int misCount = 0;

            for (int i = 0; i < sortedCards.Count - 1; i++)
            {
                if ((int)sortedCards[i + 1].points - (int)sortedCards[i].points > straightInternal)
                {
                    if (i > 0 && i < sortedCards.Count - 2)
                    {
                        return false;
                    }

                    misCount++;
                }

                if ((int)sortedCards[i + 1].points == (int)sortedCards[i].points)
                {
                    misCount++;
                }
            }

            if (misCount > cards.Count - flushSize)
            {
                return false;
            }


            return true;
        }

        private static bool IsFlush(List<Card> cards)
        {
            //int straightInternal = PlayerSingleton.Instance.straightInternal;
            int flushSize = PlayerSingleton.Instance.flushSize;
            if (cards.Count < flushSize)
            {
                return false;
            }

            bool isFlush = cards.GroupBy(c => c.color)
                .Select(group => group.ToList()).Any(group => group.Count >= flushSize);

            return isFlush;
        }

        private static bool IsFullHouse(List<Card> cards)
        {
            var groupedValues = cards.GroupBy(c => c.points)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Count())
                .ToList();

            return groupedValues.Count == 2 && groupedValues[0] == 3 && groupedValues[1] == 2;
        }

        private static bool IsFourOfAKind(List<Card> cards)
        {
            var groups = cards.GroupBy(c => c.points);
            return groups.Any(group => group.Count() == 4);
        }

        private static bool IsStraightFlush(List<Card> cards)
        {
            return IsStraight(cards) && IsFlush(cards);
        }

        private static bool IsRoyalFlush(List<Card> cards)
        {
            int straightInternal = PlayerSingleton.Instance.straightInternal;

            bool isStraightFlush = IsStraightFlush(cards);
            if (!isStraightFlush)
            {
                return false;
            }

            bool hasA = cards.Any(item => item.points == Points.Ace);
            var sortedCards = cards.OrderBy(c => c.points).ToList();

            int lastInternal = (int)sortedCards[sortedCards.Count - 1].points -
                               (int)sortedCards[sortedCards.Count - 2].points;
            bool isAIn = lastInternal <= straightInternal;


            return hasA && isAIn;
        }

        private static bool IsFiveOfAKind(List<Card> cards)
        {
            var groups = cards.GroupBy(c => c.points);
            return groups.Any(group => group.Count() == 5);
        }

        private static bool IsFiveOfAKindFlush(List<Card> cards)
        {
            return IsFiveOfAKind(cards) && IsFlush(cards);
        }

        private static bool IsFullHouseFlush(List<Card> cards)
        {
            return IsFullHouse(cards) && IsFlush(cards);
        }

        #endregion
    }
}