open System
open System.IO

// Получение всех файлов в каталоге и подкаталогах
let allFiles (path: string) =
    Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)

// Проверка, существует ли каталог и не пуст ли он
let isDirectoryValidAndNotEmpty (path: string) =
    if not (Directory.Exists(path)) then
        printfn "Kataloga net" 
        false
    else
        let files = allFiles path
        if Array.isEmpty files then
            printfn "Katalog pust"
            false
        else
            true

// Извлечение расширения файла
let allFileExtensions (filePath: string) =
    let ext = Path.GetExtension(filePath).ToLower()
    if String.IsNullOrEmpty(ext) then ".noext" else ext


// Подсчёт файлов для каждого расширения
let countExtensions (files: string seq) =
    files
    |> Seq.map allFileExtensions
    |> Seq.groupBy id
    |> Seq.map (fun (ext, seq) -> ext, Seq.length seq)

// Нахождение минимального количества встречаемости
let minCount (extensionCounts: seq<string * int>) =
    extensionCounts
    |> Seq.minBy snd
    |> snd

// Вывод результата
let printResults (minCount: int) (rareExtensions: seq<string>) =
    printfn ""
    printfn "Result:"
    printf "%d fail" minCount
    rareExtensions
    |> Seq.sortBy id
    |> Seq.iter (printfn "  %s")

[<EntryPoint>]
let main argv =
    printfn "Put: "
    let path = Console.ReadLine()

    // Проверяем существование каталога
    if not (Directory.Exists(path)) then
        printfn "Kataloga net"
        exit 1

    // Проверяем каталог
    if not (isDirectoryValidAndNotEmpty path) then
        exit 1

    let files = allFiles path

    let extensionCounts = countExtensions files

    let minCount = minCount extensionCounts

    // Фильтр на расширения с минимальной частотой
    let rareExtensions =
        extensionCounts
        |> Seq.filter (fun (_, count) -> count = minCount)
        |> Seq.map fst

    printResults minCount rareExtensions

    0 
