open System

type BinTree<'a> =
    | Node of 'a * BinTree<'a> * BinTree<'a>
    | Empty

// Функция вставки элемента в бинарное дерево 
let rec insert value tree =
    match tree with
    | Empty -> Node(value, Empty, Empty) 
    | Node(currentValue, left, right) when value < currentValue ->
        Node(currentValue, insert value left, right) // В левое поддерево
    | Node(currentValue, left, right) when value >= currentValue ->
        Node(currentValue, left, insert value right) // В правое поддерево

// Функция для заполнения дерева N случайными числами в диапазоне [min, max]
let fillWithRandom n min max =
    let random = System.Random()
    let rec fillHelper count tree =
        if count = 0 then
            tree
        else
            let randomValue = random.Next(min, max + 1)
            let newTree = insert randomValue tree
            fillHelper (count - 1) newTree
    fillHelper n Empty

// Упорядоченный вывод
let rec printTree tree =
    match tree with
    | Empty -> []
    | Node(value, left, right) ->
        (printTree left) @ [value] @ (printTree right)

let rec map f tree =
    match tree with
    | Empty -> Empty
    | Node(x, l, r) ->
        Node(
        f x,
        map f l,
        map f r)

// Функция обработки элемента
let processNumber (num: int) =
    let digits = string num |> Seq.map (fun c -> int c - int '0') |> List.ofSeq
    let processed =
        digits
        |> List.map (fun d ->
            if d = 0 then 1
            else d - 1)
    let result =
        processed
        |> List.fold (fun acc d -> acc * 10 + d) 0
    result

// Функция обработки дерева
let processTree tree =
    map processNumber tree

// Основная функция
[<EntryPoint>]
let main argv =
    // Заполняем дерево случайными числами от 0 до 1000
    let randomTree = fillWithRandom 10 0 1000
    printfn "BinTree: %A" (printTree randomTree)

    // Обрабатываем дерево
    let processedTree = processTree randomTree
    printfn "BinTree: %A" (printTree processedTree)

    0