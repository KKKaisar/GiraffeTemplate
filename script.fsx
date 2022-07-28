let a = 3
let b = 0.3
let s = ""
let list1 = [ 2; 3; 4 ]
let newList = 1 :: list1
let concatLists = [ 0; 1 ] @ list1

let seq1 =
    seq {
        2
        3
        4
    }




let square x = x * x

let add x y = x + y

let evens list =
    let isEven x = x % 2 = 0
    List.filter isEven list

let sumOfSquaresTo100 = List.sum <| List.map square [ 1..100 ]

let sumOfSquaresTo100piped = [ 1..100 ] |> List.map square |> List.sum

let sumOfSquaresTo100withFun =
    [ 1..100 ]
    |> List.map (fun x -> x * x)
    |> List.sum

let rec factorial x =
    match x with
    | 0
    | 1 -> 1
    | _ -> x * factorial (x - 1)

let validValue = Some(99)

let invalidValue = None

let optionPatternMatch input =
    match input with
    | Some i -> $"{i}"
    | None -> "Missing"

let tuple = 1, 2, 34
let tuple2 = 1, "2", true, None, Some 5
let tuple3 = (1, 2, 3)

let list23 = [ (1, 2); (2, 3) ]



printfn $"{3} {tuple2} {tuple3}"
