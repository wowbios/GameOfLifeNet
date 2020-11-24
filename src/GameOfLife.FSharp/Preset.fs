module GameOfLife.FSharp.Preset

open System
open GameOfLife.Abstractions

type Preset(coordinates) =
    let initF field =
        let i = Array2D.length1 field
        let j = Array2D.length2 field
        
        coordinates field (i, j)
        |> Seq.iter (fun (x, y) ->
            Array2D.set field x y true)
    
    interface IPreset with
        member this.InitializeField(field) = initF field

type GliderAtTheMiddlePreset() = inherit Preset(fun _ (i, j) -> [
        i-1, j
        i, j+1
        i+1, j-1
        i+1, j
        i+1, j+1
    ])

type StickPreset() = inherit Preset(fun _ (i, j) -> [
        1, 2
        2, 2
        3, 2
    ])

let private randomPreset field i j percent =
    let w = Array2D.length1 field
    let h = Array2D.length2 field
    
    let rnd = Random()
    let total = w * h * percent / 100
    
    List.init total (fun _ -> (rnd.Next(0, w), rnd.Next(0, h)))
    
type RandomPreset(percent) =
    inherit Preset(fun field (i, j) -> randomPreset field i j percent)
    do
    if percent < 0 || percent > 100 then
        raise (ArgumentException("Fulfill must be positive and less than 100", "percent"))
        
let private randomAreasPreset(field, i, j, size, count, preset:IPreset) =
    let w = Array2D.length1 field
    let h = Array2D.length2 field
    
    let aw = w * size / 100
    let ah = h * size / 100
    
    let rnd = Random()
    
    let wi = rnd.Next(0, w)
    let hi = rnd.Next(0, h)
    [|for _ in 0..count - 1 do
        let area = Array2D.init aw ah (fun _ _ -> false)
        preset.InitializeField(area) 
        for wc in 0..aw - 1 do
            for hc in 0..ah - 1 do
                let x = (wi + wc) % w
                let y = (hi + hc) % h
                if Array2D.get area wc hc then
                    yield (x, y)|]
    |> Array.toList

type RandomAreas(size, count, preset: IPreset) =
    inherit Preset(fun field (i,j) ->
        randomAreasPreset(field, i, j, size, count, preset))