module Day06.Part2

open FParsec

let numberList = sepEndBy pint32 spaces
let numberKem = numberList |>> (Seq.map (sprintf "%i") >> Seq.reduce (+) >> uint64)
let time = pstring "Time:" >>. spaces >>. numberKem
let distance = pstring "Distance:" >>. spaces >>. numberKem

let run input =
    let time, distance = CharStream.ParseString(input, 0, String.length input, (time .>>. distance), (), null).Result
    [1UL..time - 1UL]
    |> Seq.map (fun i -> (time - i) * i)
    |> Seq.filter ((<) distance)
    |> Seq.length
