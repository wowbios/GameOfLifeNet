module GameOfLife.FSharp.ConwayRuleset

open GameOfLife.Abstractions

type public ConwaysRuleset() =
    
    let isAlive(field: bool[,], x: int, y: int) = field.[x, y]
    
    let getClosestAliveCountF
            (field:bool[,],
            x,
            y,
            height,
            width)
            =
        let alive = 0
        
        let check x y =
            match isAlive(field, x, y) with
            | true -> alive = alive + 1
        
        let i1 = (x + width - 1) % width;
        let i3 = (x + width + 1) % width;
        let j1 = (y + height - 1) % height;
        let j3 = (y + height + 1) % height
        
        check i1 j1
        check i1 y 
        check i1 j3
        
        check x j1 
        check x j3 
        
        check i3 j1
        check i3 y 
        check i3 j3
        
        alive
        
            
    interface IRuleset with
        member this.IsAlive(field: bool[,], x: int, y: int) =
            let width = field.GetLength 0
            let height = field.GetLength 1
            
            let alive = getClosestAliveCountF(
                            field,
                            x,
                            y,
                            height,
                            width)
             
            let isAliveCell = isAlive(field, x, y)                
            match isAliveCell with
            | true ->
                match alive <> 2 && alive <> 3 with
                | true -> isAliveCell = false
            | false ->
                match alive = 3 with
                | true -> isAliveCell = true
            
            |> ignore
            
            isAliveCell