module Day09.Part1

open FParsec

let numberSequence = sepEndBy1 pint32 (pchar ' ') .>> opt newline

let diffs<'a> =
    List.pairwise >> List.map (fun (a, b) -> b - a)

let notAllZeros = List.exists ((<>) 0)

let rec deriveWhile derive predicate x = seq {
    yield x
    match derive x with
    | derived when predicate derived -> yield! deriveWhile derive predicate derived
    | derived -> yield derived
}

let rec appendToLast v = function
    | head::[] -> [List.append head v]
    | head::tail -> [head]@(appendToLast v tail)

let combine a b =
    let newVal = (List.last a) + (List.last b)
    List.append a [newVal]
    
let run (input: string) =
    match run (many numberSequence) input with
    | Success(sequences, _, _) ->
        sequences
        |> List.map (deriveWhile diffs notAllZeros >> List.ofSeq)
        |> List.map (appendToLast [0])
        |> List.map (List.reduceBack combine)
        |> List.map List.last
        |> List.sum
    | Failure(message, _, _) -> failwith message