open System

// Функция для чтения натурального числа с проверкой ввода
let rec readnatural (text: string) =
    printf "%s" text
    let input = Console.ReadLine()
    match Int32.TryParse(input) with
    | (true, n) when n > 0 ->
        if input = n.ToString() then
            n
        else
            printfn "Ошибка: число не должно начинаться с нуля."
            readnatural text
    | (true, _) ->
        printfn "Ошибка: число должно быть натуральным."
        readnatural text
    | (false, _) ->
        printfn "Ошибка: введено не число."
        readnatural text

// Функция для создания последовательности строк с отложенным вычислением
let createsequence () =
    lazy (
        seq {
            let text = "Введите кол-во элементов для добавления в последовательность: "
            let number = readnatural text
            if number > 0 then
                for i in 1 .. number do
                    let prompt = sprintf "Введите %d-ю строку последовательности: " i
                    printf "%s" prompt
                    let element = Console.ReadLine()
                    yield element
        }
    )

// Функция для подсчета количества строк заданной длины с использованием Seq.fold
let countstrings targetLength (sequence: seq<string>) =
    sequence
    |> Seq.fold (fun (count, _) str -> 
                   if str.Length = targetLength then (count + 1, ()) else (count, ())) (0, ())
    |> fst

// Основная функция
let main () =
    let sequence = createsequence ()
    printfn "Введите длину строк, которые нужно подсчитать:"
    let targetLength = readnatural ""

    let result = countstrings targetLength sequence.Value
    printfn "Кол-во строк длины %d: %d" targetLength result

// Запуск программы
main ()