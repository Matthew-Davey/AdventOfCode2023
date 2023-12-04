open System.IO

let sample = """467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..
"""

assert (Day03.Part1.run sample = 4361)
assert (Day03.Part2.run sample = 467835)

let input = File.ReadAllText "./input.txt"

printfn $"Part1: %i{Day03.Part1.run input}"
printfn $"Part2: %i{Day03.Part2.run input}"
