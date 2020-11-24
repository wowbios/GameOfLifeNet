namespace GameOfLife.FSharp.Game

open GameOfLife.Abstractions

type ChangeEvent(x, y, isAlive) =
    interface IChangeEvent with
        member this.X = x
        member this.Y = y
        member this.IsAlive = isAlive

type GameState(generation, events) =
    interface IGameState with
        member this.Generation = generation
        member this.Events = events
        
type GameSettings(width, height, preset, render, ruleset) =
    interface IGameSettings with
        member this.Width = width
        member this.Height = height
        member this.Preset = preset
        member this.Render = render
        member this.Ruleset = ruleset
        
type Game(settings: IGameSettings) =
    let mutable field = Array2D.init settings.Width settings.Height (fun _ _ -> false)
    
    let mutable generation = 0L
    
    let render events =
        let state = GameState(generation, events)
        settings.Render.Render state
    
    let prepare() =
        let w = Array2D.length1 field
        let h = Array2D.length2 field
        
        settings.Preset.InitializeField(field)
        render [| for i in 0..w - 1 do
                  for j in 0..h - 1 do
                    ChangeEvent(i, j, field.[i, j]) |]
        
    let makeNextGen() =
        let events = ResizeArray()
        let newField = Array2D.copy field
        let width = Array2D.length1 field
        let height = Array2D.length2 field
        
        let processRow rowIndex rowWidth field  =        
            seq {
                for columnIndex in 0..rowWidth - 1 do
                    let current = Array2D.get field rowIndex columnIndex
                    let isA = settings.Ruleset.IsAlive(field, rowIndex, columnIndex)
                    if current <> isA then
                        newField.[rowIndex, columnIndex] <- isA
                        ChangeEvent(rowIndex, columnIndex, isA)                    
            }
        
        seq {
            for rowIndex in 0..height - 1 do
                processRow rowIndex width field |> async.Return
        }
        |> Async.Parallel
        |> Async.RunSynchronously
        |> Array.iter (Seq.iter events.Add)
        
        field <- newField
        generation <- generation + 1L
        
        if events.Count > 0 then
            render (events
                    |> Seq.map (fun e -> e :> IChangeEvent)
                    |> Seq.toArray)
    
    interface IGame with
        member this.Prepare() = prepare()
        member this.MakeNextGeneration() = makeNextGen()