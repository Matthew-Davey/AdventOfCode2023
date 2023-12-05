module Day05.Part2

open FParsec

type RangeOffset = { Index: int64; Length: int64; Offset: int64 }

let seedRange = (pint64 .>> pchar ' ') .>>. pint64 |>> fun (x, length) -> [x .. x + length]
let seeds = pstring "seeds: " >>. (sepBy1 seedRange (pchar ' ')) .>> spaces |>> List.concat
let range = sepBy1 pint64 (pchar ' ') |>> fun xs -> { Index = xs[1]; Length = xs[2]; Offset = xs[0] - xs[1] }
let map   = manyCharsTill anyChar newline >>. (sepEndBy1 range newline)
let maps  = (sepEndBy1 map (many newline))

let contains x { Index = index; Length = length } = index <= x && x < (index + length)

let applyMap x ranges =
    match Seq.tryFind (contains x) ranges with
    | Some { Offset = offset } -> x + offset
    | None -> x

let run input =
    let seeds, maps = CharStream.ParseString(input, 0, String.length input, (seeds .>>. maps), (), null).Result
    (Seq.map (fun seed -> Seq.fold applyMap seed maps) >> Seq.min) seeds
