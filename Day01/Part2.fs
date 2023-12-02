module Day01.Part2

open System
open System.Globalization
open FSharpPlus

let convertSpelledOutNumerals =
    String.replace "one" "o1e"
    >> String.replace "two" "t2"
    >> String.replace "three" "t3e"
    >> String.replace "four" "4"
    >> String.replace "five" "5e"
    >> String.replace "six" "6"
    >> String.replace "seven" "7n"
    >> String.replace "eight" "e8"
    >> String.replace "nine" "9"
    
let filterNumerals = Seq.filter Char.IsDigit
let numeralsToValues = Seq.map CharUnicodeInfo.GetDigitValue
let pickFirstAndLast xs = (Seq.head xs, Seq.last xs)
let combineNumerals (a, b) = (a * 10) + b

let run : string seq -> int =
    Seq.map convertSpelledOutNumerals
    >> Seq.map filterNumerals
    >> Seq.map numeralsToValues
    >> Seq.map pickFirstAndLast
    >> Seq.map combineNumerals
    >> Seq.sum
