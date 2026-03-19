open System

let buildSequence () =
    printfn "vvod (000 - end):"
    let rec collect () = seq {
        let input = System.Console.ReadLine()

        match input with
        | "000" -> ()  
        | _ ->
            match System.Int32.TryParse(input) with
            | (true, number) when number >= 0 && number <= 9 ->
                yield number  
                yield! collect ()  
            | (true, _) ->
                printfn "err"
                yield! collect () 
            | (false, _) ->
                printfn "err"
                yield! collect () 
    }
    collect ()

let createNumberFromEvenDigits (sequence: seq<int>) =
    sequence
    |> Seq.filter (fun x -> x % 2 = 0) 
    |> Seq.fold (fun acc digit -> acc * 10 + digit) 0 

// Основная функция
[<EntryPoint>]
let main argv =

    let result = buildSequence ()

    let res = createNumberFromEvenDigits result
    printfn "Res: %d" res

    0
