module Day02.Part2

open FParsec

let parseColour =
    pstring "red" <|> pstring "green" <|> pstring "blue"
    
let parseRoll =
    pint32 .>>. (spaces >>. parseColour .>> optional (pchar ',' <|> pchar ';') .>> spaces)

let parseGame =
    pstring "Game " >>. pint32 >>. pstring ": " >>. many parseRoll .>> optional newline

let max colourFilter =
    List.filter (snd >> ((=) colourFilter))
    >> List.map fst
    >> List.max
    
let run input =
    match run (many parseGame) input with
    | Failure (message, _, _) -> raise (System.FormatException(message))
    | Success (rolls, _, _)  ->
        rolls
        |> List.map (fun rolls -> [max "red" rolls; max "green" rolls; max "blue" rolls])
        |> List.map (List.reduce (*))
        |> List.sum
