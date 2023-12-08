open System.IO

let sample1 = """RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)"""

let sample2 = """LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)"""

assert (Day08.Part1.run sample1 = 2)
assert (Day08.Part2.run sample2 = bigint 6)

let input = File.ReadAllText "./input.txt"

printfn $"Part1: %i{Day08.Part1.run input}"
printfn $"Part2: %A{Day08.Part2.run input}"