module Day06.Part1

open FParsec

let numberList = sepEndBy pint32 spaces
let times = pstring "Time:" >>. spaces >>. numberList
let distances = pstring "Distance:" >>. spaces >>. numberList

let countWinningOptions (time, distance) =
    [1..time - 1]
    |> Seq.map (fun i -> (time - i) * i)
    |> Seq.filter ((<) distance)
    |> Seq.length

let run input =
    let times, distances = CharStream.ParseString(input, 0, String.length input, (times .>>. distances), (), null).Result
    Seq.zip times distances
    |> Seq.map countWinningOptions
    |> Seq.reduce (*)
