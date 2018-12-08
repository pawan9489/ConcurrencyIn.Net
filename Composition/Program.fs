// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System.Threading.Tasks

[<EntryPoint>]
let main argv = 
   
    let add4 x = x + 4

    let mult3 x = x * 3

    // Pipelining
    let res = 12 |> add4 |> mult3

    //printfn "%d" res
    // Composition
    let c = add4 >> mult3
    
    //printfn "%d" (c(12))

    for i = 1 to 10 do
        Task.Factory.StartNew(fun () -> printfn "%d" i)

    System.Console.ReadLine()
    0 // return an integer exit code
