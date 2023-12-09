module Day09.Part2

open FParsec

let numbers = sepBy1 pint32 (pchar ' ') .>> opt newline

let pairwiseMap fn = List.pairwise >> List.map (fun (a, b) -> fn b a)

let rec deriveWhile predicate derive x = seq {
    yield x
    match derive x with
    | derived when predicate derived -> yield! deriveWhile predicate derive derived
    | derived -> yield derived }

let rec prependToLast v = function
    | [ head ]   -> [v @ head]
    | head::tail -> [head] @ (prependToLast v tail)

let extrapolateBackwards x y = [(List.head x) - (List.head y)] @ x
    
let run input =
    CharStream.ParseString(input, 0, String.length input, (many numbers), (), null).Result
    |> List.map (deriveWhile (List.exists ((<>) 0)) (pairwiseMap (-)) >> List.ofSeq)
    |> List.map (prependToLast [0])
    |> List.map (List.reduceBack extrapolateBackwards)
    |> List.sumBy List.head
