module Day04.Part1

open FParsec

let numbers count = parray count (spaces >>. pint32) |>> Set.ofArray
let prefix = skipManyTill anyChar (pstring ": ")
let separator = pstring " | "
let card count1 count2 = between prefix spaces (numbers count1 .>>. (separator >>. numbers count2))

let run input count1 count2 =
    CharStream.ParseString(input, 0, String.length input, many (card count1 count2), (), null).Result
    |> Seq.map (fun (xs, ys) -> Set.intersect xs ys)
    |> Seq.map Set.count
    |> Seq.filter ((<) 0)
    |> Seq.fold (fun acc x -> acc + (1 <<< (x - 1))) 0
    