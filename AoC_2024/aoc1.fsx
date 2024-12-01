open System.Text.RegularExpressions

let input = System.IO.File.ReadLines "AoC_2024/input_1.txt"


let splitStringToNumbers (input: string) =
    let regexMatch = Regex.Match(input, @"(\d+)\s+(\d+)")
    if regexMatch.Success then
        let number1 = int regexMatch.Groups.[1].Value
        let number2 = int regexMatch.Groups.[2].Value
        number1, number2
    else
        failwith "Input string does not match the expected format"

let list1, list2 =
    input
    |> Seq.map splitStringToNumbers
    |> Seq.toList
    |> List.unzip

let answer1 =
    List.zip (List.sort list1) (List.sort list2)
    |> List.sumBy (fun (x, y) -> abs (x - y))

let mapNumberToOccurrences = list2 |> List.countBy id |> Map.ofList

let answers2 =
    list1
    |> List.fold (fun acc x ->
        match mapNumberToOccurrences.TryFind x with
        | Some y -> x * y + acc
        | None -> acc) 0
