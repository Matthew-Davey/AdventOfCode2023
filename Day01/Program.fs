open System.IO

let sample1 = [
    "1abc2"
    "pqr3stu8vwx"
    "a1b2c3d4e5f"
    "treb7uchet"
]

let sample2 = [
    "two1nine"
    "eightwothree"
    "abcone2threexyz"
    "xtwone3four"
    "4nineeightseven2"
    "zoneight234"
    "7pqrstsixteen"
]

assert (Day01.Part1.run sample1 = 142)
assert (Day01.Part2.run sample2 = 281)

let input = File.ReadAllLines("./input.txt")

printfn $"Part1: %i{Day01.Part1.run input}"
printfn $"Part2: %i{Day01.Part2.run input}"