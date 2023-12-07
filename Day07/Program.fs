open System.IO

let sample = """32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483"""

assert (Day07.Part1.run sample = 6440)
assert (Day07.Part2.run sample = 5905)

let input = File.ReadAllText "./input.txt"

printfn $"Part1: %i{Day07.Part1.run input}"
printfn $"Part2: %i{Day07.Part2.run input}"