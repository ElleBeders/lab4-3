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

let rec fold folder acc tree =
    match tree with
    | Empty -> acc
    | Node(value, left, right) ->
        let accLeft = fold folder acc left
        let accCurrent = folder accLeft value
        fold folder accCurrent right

// Ввод дерева
let buildTreeFromInput () =
    printfn "Vvod tree:"
    printfn "(000 - end)"
    let rec inputLoop tree =
        let input = System.Console.ReadLine()
        if input = "000" then
            tree
        else
            let newTree = insert input tree
            inputLoop newTree
    inputLoop Empty

// Функция подсчёта узлов, заканчивающихся на заданный символ
let countNodesEndingWithFold (charToFind: char) tree =
    fold
        (fun acc value ->
            if not (System.String.IsNullOrEmpty value) && value.[value.Length - 1] = charToFind
            then acc + 1
            else acc)
        0
        tree

// Основная функция
[<EntryPoint>]
let main argv =
    // Заполняем дерево
    let userTree = buildTreeFromInput()
    printfn "BinTree: %A" (printTree userTree)

    printfn "Char:"
    let searchCharInput = System.Console.ReadLine()
    let searchChar = searchCharInput.[0] 

    // Подсчитываем узлы
    let count = countNodesEndingWithFold searchChar userTree
    printfn "End '%c': %d" searchChar count

    0