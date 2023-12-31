module Day09.Part1

open FParsec

let numbers = sepBy1 pint32 (pchar ' ') .>> opt newline

let pairwiseMap fn = List.pairwise >> List.map (fun (a, b) -> fn b a)

let rec deriveWhile predicate derive x = seq {
    yield x
    match derive x with
    | derived when predicate derived -> yield! deriveWhile predicate derive derived
    | derived -> yield derived }

let rec appendToLast v = function
    | [ head ]   -> [head @ v]
    | head::tail -> [head] @ (appendToLast v tail)

let extrapolateForwards x y = x @ [(List.last x) + (List.last y)]
    
let run input =
    CharStream.ParseString(input, 0, String.length input, (many numbers), (), null).Result
    |> List.map (deriveWhile (List.exists ((<>) 0)) (pairwiseMap (-)) >> List.ofSeq)
    |> List.map (appendToLast [0])
    |> List.map (List.reduceBack extrapolateForwards)
    |> List.sumBy List.last
