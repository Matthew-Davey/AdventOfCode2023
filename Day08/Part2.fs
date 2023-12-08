module Day08.Part2

open FSharpPlus
open FSharpPlus.Math.Generic
open FParsec

let instructions = manyTill (charReturn 'L' 0 <|> charReturn 'R' 1) newline .>> spaces
let nodeIdentifier = (parray 3 anyChar) |>> String.ofArray
let node = nodeIdentifier >>= fun identifier ->
    pstring " = " >>. between (pchar '(') (pchar ')') (sepBy nodeIdentifier (pstring ", ")) >>= fun lr ->
    preturn (identifier, [lr[0]; lr[1]])
let nodes = (sepEndBy node newline) |>> Map.ofList

let rec followInstructions instructions nodes step node =
    if String.endsWith "Z" node then (toBigInt step)
    else
        Map.find node nodes
        |> item (nth (step % length instructions) instructions) 
        |> followInstructions instructions nodes (step + 1)

let lcm2 a b = a * b / (gcd a b)

let rec lcm = function
    | [a;b]      -> lcm2 a b
    | head::tail -> lcm2 head (lcm tail)
    | []         -> bigint 1

let run input =
    let instructions, nodes = CharStream.ParseString(input, 0, length input, (instructions .>>. nodes), (), null).Result
    nodes
    |> (Map.keys >> Seq.filter (String.endsWith "A") >> List.ofSeq) 
    |> List.map (followInstructions instructions nodes 0)
    |> lcm
