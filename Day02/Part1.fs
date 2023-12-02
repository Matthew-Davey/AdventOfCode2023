module Day02.Part1

open FParsec

let parseColour =
    pstring "red" <|> pstring "green" <|> pstring "blue"
    
let parseRoll =
    pint32 .>>. (spaces >>. parseColour .>> optional (pchar ',' <|> pchar ';') .>> spaces)

let parseGame =
    pstring "Game " >>. pint32 >>. pstring ": " >>. many parseRoll .>> optional newline
    
let rollValid (number, colour) =
    match colour with
    | "red" -> number <= 12
    | "green" -> number <= 13
    | "blue" -> number <= 14
    
let run input =
    match run (many parseGame) input with
    | Failure (message, _, _) -> raise (System.FormatException(message))
    | Success (games, _, _)  ->
        List.indexed games
        |> List.filter (snd >> List.forall rollValid)
        |> List.map fst
        |> List.sumBy ((+) 1)
