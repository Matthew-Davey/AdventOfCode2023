module Day08.Part1

open FSharpPlus
open FParsec

let instructions = manyTill (charReturn 'L' 0 <|> charReturn 'R' 1) newline .>> spaces
let nodeIdentifier = (parray 3 asciiUpper) |>> String.ofArray
let node = nodeIdentifier >>= fun identifier ->
    pstring " = " >>. between (pchar '(') (pchar ')') (sepBy nodeIdentifier (pstring ", ")) >>= fun lr ->
    preturn (identifier, [lr[0]; lr[1]])
let nodes = (sepEndBy node newline) |>> Map.ofList

let rec followInstructions instructions nodes step = function
    | "ZZZ" -> step
    | node ->
        Map.find node nodes
        |> item (nth (step % length instructions) instructions) 
        |> followInstructions instructions nodes (step + 1)

let run input =
    let instructions, nodes = CharStream.ParseString(input, 0, length input, (instructions .>>. nodes), (), null).Result
    followInstructions instructions nodes 0 "AAA"
