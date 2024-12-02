open System
open System.Text.RegularExpressions

let input = System.IO.File.ReadLines "AoC_2024/input_2.txt"

let codes = input |> Seq.map (fun x -> x.Split(' ') |> Array.map int |> Array.toList) |> Seq.toList

let isSorted (code: int list) (compare: int -> int -> bool) =
    match code with
    | [] -> true
    | x::xs ->
        let rec isSorted' (code: int list) lastValue =
            match code with
            | [] -> true
            | x::xs -> if compare x lastValue && abs (x - lastValue) <= 3 then isSorted' xs x else false
        isSorted' xs x

let isDescending code = isSorted code (>=)
let isAscending code = isSorted code (<=)

let isCodeValid code =
    isSorted code (>) || isSorted code (<)

let answer1 = codes |> List.filter isCodeValid |> List.length

let permutationsOfList (code: int list) =
    let rec permutationsOfList' (code: int list) i acc =
        if i = code.Length then acc
        else
            let newAcc = code |> List.removeAt i
            permutationsOfList' code (i+1) (newAcc :: acc)
    permutationsOfList' code 0 [code]

let isCodeValid' code =
   let permutations = permutationsOfList code
   permutations |> List.exists isCodeValid

let answer2 = codes |> List.filter (fun x -> isCodeValid' x) |> List.length
