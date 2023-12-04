module Day03.Part1

open FParsec

let position = fun (stream: CharStream<_>) ->
    Reply ((int stream.Position.Column - 1, int stream.Position.Line - 1))

let locateNumbers input =
    let noise = skipMany (anyOf ".-@*/&#%+=$\n")
    let pint32xyw = pipe3 position pint32 position (fun (x, y) i (x2, _) -> ((x, y, x2 - x), i))
    let locateNumbers = noise >>. sepEndBy pint32xyw noise
    CharStream.ParseString(input, 0, String.length input, locateNumbers, (), null).Result

let locateSymbols input =
    let noise = skipMany (anyOf ".0123456789\n")
    let locateSymbols = noise >>. sepEndBy (position .>> anyChar) noise
    CharStream.ParseString(input, 0, String.length input, locateSymbols, (), null).Result

let boundingBox ((x, y, width), n) =
    ((x - 1, y - 1, x + width, y + 1), n)

let pointInsideBox (x1, y1, x2, y2) (x, y) =
    x >= x1 && x <= x2 && y >= y1 && y <= y2

let run input =
    let numbers = locateNumbers input
    let symbols = locateSymbols input

    numbers
    |> List.map boundingBox
    |> List.filter (fun (box, _) -> List.exists (pointInsideBox box) symbols)
    |> List.map snd
    |> List.sum
