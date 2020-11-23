module GameOfLife.FSharp.Ruleset

open GameOfLife.Abstractions

type public ConwaysRuleset() =
    let getClosestAliveCountF field x y =
        let isAlive (x, y) = if Array2D.get field x y then 1 else 0
        let width = Array2D.length1 field
        let height = Array2D.length2 field
                
        let i1 = (x + width - 1) % width;
        let i3 = (x + width + 1) % width;
        let j1 = (y + height - 1) % height;
        let j3 = (y + height + 1) % height
        
        [ i1, j1
          i1, y
          i1, j3
          
          x, j1
          x, j3
          
          i3, j1
          i3, y
          i3, j3]
        |> List.sumBy isAlive
        
    let isAlive field x y =
        let alive = getClosestAliveCountF field x y
        match Array2D.get field x y with
            | true when alive <> 2 && alive <> 3 -> false
            | false when alive = 3 -> true
            | result -> result
    
    interface IRuleset with
        member this.IsAlive(field, x, y) = isAlive field x y