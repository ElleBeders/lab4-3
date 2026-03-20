open System

// Словарь:
let romanToDecimal = 
    dict [
        ("I", 1)
        ("II", 2)
        ("III", 3)
        ("IV", 4)
        ("V", 5)
        ("VI", 6)
        ("VII", 7)
        ("VIII", 8)
        ("IX", 9)
    ]

let buildSequence () =
    let rec collect () = seq {
        let input = System.Console.ReadLine()

        match input with
        | "000" -> ()  
        | _ ->
            yield input 
            yield! collect () 
    }
    collect ()

let convertRomanToDecimal (roman: string) =
    match romanToDecimal.TryGetValue(roman) with
    | true, value -> Some value
    | false, _ -> None  

let printSequence seq =
        let items = seq |> Seq.map string |> String.concat " "
        printfn "%s" items

// Основная функция
[<EntryPoint>]
let main argv =
    printfn "vvod (000 - end):"
    
    let romanSeq = buildSequence ()

    let decimalSeq =
        romanSeq
        |> Seq.map convertRomanToDecimal  
        |> Seq.choose id                

    printSequence decimalSeq


    0
