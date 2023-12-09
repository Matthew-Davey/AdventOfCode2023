open System.IO

let sample = """0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45"""

assert (Day09.Part1.run sample = 114)
assert (Day09.Part2.run sample = 2)

let input = File.ReadAllText "./input.txt"

printfn $"Part1: %i{Day09.Part1.run input}"
printfn $"Part2: %i{Day09.Part2.run input}"