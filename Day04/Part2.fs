module Day04.Part2

open FParsec

let numbers count = parray count (spaces >>. pint32) |>> Set.ofArray
let prefix = skipManyTill anyChar (pstring ": ")
let separator = pstring " | "
let card count1 count2 = between prefix spaces (numbers count1 .>>. (separator >>. numbers count2))

let run input count1 count2 =
    let scores =
        CharStream.ParseString(input, 0, String.length input, many (card count1 count2), (), null).Result
        |> List.map (fun (xs, ys) -> Set.intersect xs ys |> Set.count)

    let copies = Array.replicate (List.length scores) 1

    for i = 0 to List.length scores - 1 do
        if scores[i] > 0 then
            for j = 1 to scores[i] do
                copies[i + j] <- copies[i] + copies[i + j]
                
    Array.sum copies
