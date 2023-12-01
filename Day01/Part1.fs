module Day01.Part01

open System
open System.Globalization

let filterNumerals = Seq.filter Char.IsDigit
let numeralsToValues = Seq.map CharUnicodeInfo.GetDigitValue
let pickFirstAndLast xs = (Seq.head xs, Seq.last xs)
let combineNumerals (a, b) = (a * 10) + b

let run : string seq -> int =
    Seq.map filterNumerals
    >> Seq.map numeralsToValues
    >> Seq.map pickFirstAndLast
    >> Seq.map combineNumerals
    >> Seq.sum
