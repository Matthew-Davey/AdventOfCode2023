module Day07.Part2

open FParsec

type Card = Two | Three | Four | Five | Six | Seven | Eight | Nine | Ten | Joker | Queen | King | Ace

let strengthOfCard card =
    Seq.findIndex ((=) card) [Joker; Two; Three; Four; Five; Six; Seven; Eight; Nine; Ten; Queen; King; Ace]

let strengthOfHand =
    List.sortByDescending strengthOfCard
    >> List.countBy id
    >> List.sortByDescending snd
    >> function
    | [(_, 5)]                                     -> 6
    | [(_, 4); (Joker, 1)]                         -> 6
    | [(_, 3); (Joker, 2)]                         -> 6
    | [(Joker, 3); (_, 2)]                         -> 6
    | [(Joker, 4); (_, 1)]                         -> 6
    | [(_, 4); (_, 1)]                             -> 5
    | [(_, 3); (_, 1); (Joker, 1)]                 -> 5
    | [(_, 2); (Joker, 2); (_, 1)]                 -> 5
    | [(Joker, 3); (_, 1); (_, 1)]                 -> 5
    | [(_, 3); (_, 2)]                             -> 4
    | [(_, 2); (_, 2); (Joker, 1)]                 -> 4
    | [(_, 3); (_, 1); (_, 1)]                     -> 3
    | [(_, 2); (_, 1); (_, 1); (Joker, 1)]         -> 3
    | [(Joker, 2); (_, 1); (_, 1); (_, 1)]         -> 3
    | [(_, 2); (_, 2); (_, 1)]                     -> 2
    | [(_, 2); (_, 1); (_, 1); (_, 1)]             -> 1
    | [(_, 1); (_, 1); (_, 1); (_, 1); (Joker, 1)] -> 1
    | _                                            -> 0

let strengthOfCards = List.map strengthOfCard
    
let card = anyOf "23456789TJQKA" |>> function
    | '2' -> Two  | '3' -> Three | '4' -> Four  | '5' -> Five  | '6' -> Six | '7' -> Seven | '8' -> Eight
    | '9' -> Nine | 'T' -> Ten   | 'J' -> Joker | 'Q' -> Queen | 'K' -> King | 'A' -> Ace
    
let hand = parray 5 card |>> List.ofArray
let line = hand .>>. (spaces >>. pint32 .>> opt newline)

let run input =
    CharStream.ParseString(input, 0, String.length input, (many line), (), null).Result
    |> Seq.sortBy (fun (hand, _) -> (strengthOfHand hand, strengthOfCards hand))
    |> Seq.mapi (fun index (_, bid) -> (index + 1) * bid)
    |> Seq.reduce (+)
