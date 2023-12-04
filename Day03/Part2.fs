module Day03.Part2

open FParsec

let position = fun (stream: CharStream<_>) ->
    Reply ((int stream.Position.Column - 1, int stream.Position.Line - 1))

let locateNumbers input =
    let noise = skipMany (anyOf ".-@*/&#%+=$\n")
    let pint32xyw = pipe3 position pint32 position (fun (x, y) i (x2, _) -> ((x, y, x2 - x), i))
    let locateNumbers = noise >>. sepEndBy pint32xyw noise
    CharStream.ParseString(input, 0, String.length input, locateNumbers, (), null).Result

let locateGears input =
    let noise = skipMany (satisfy ((<>) '*'))
    let locateGears = noise >>. sepEndBy (position .>> pchar '*') noise
    CharStream.ParseString(input, 0, String.length input, locateGears, (), null).Result

let boundingBox ((x, y, width), n) =
    ((x - 1, y - 1, x + width, y + 1), n)

let pointInsideBox (x1, y1, x2, y2) (x, y) =
    x >= x1 && x <= x2 && y >= y1 && y <= y2

let adjacentNumbers xy =
    List.map boundingBox
    >> List.filter (fun (box, _) -> pointInsideBox box xy)
    >> List.map snd

let run input =
    let numbers = locateNumbers input
    let gears = locateGears input

    gears
    |> List.map (fun xy -> adjacentNumbers xy numbers)
    |> List.filter ((<) 1 << List.length)
    |> List.map (List.reduce (*))
    |> List.sum
