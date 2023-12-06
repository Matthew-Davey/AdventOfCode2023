open System.IO

let sample = """Time:      7  15   30
Distance:  9  40  200"""

assert (Day06.Part1.run sample = 288)
assert (Day06.Part2.run sample = 71503)

let input = File.ReadAllText "./input.txt"

printfn $"Part1: %i{Day06.Part1.run input}"
printfn $"Part2: %i{Day06.Part2.run input}"