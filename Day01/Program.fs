open System.IO

let input = File.ReadAllLines("./input.txt")

printfn $"Part1: %i{Day01.Part01.run input}"
printfn $"Part2: %i{Day01.Part02.run input}"