namespace GameOfLife.FSharp.Game

open System.Collections.Concurrent
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
        // single thread
//        let events = ResizeArray<IChangeEvent>()
//        let newField =
//            field
//            |> Array2D.mapi (fun x y value ->
//                let isAlive = settings.Ruleset.IsAlive(field, x , y)
//                match value = isAlive with
//                | true -> value
//                | false -> ChangeEvent(x,y, isAlive) |> events.Add
//                           isAlive)

        // multi thread
        let events = ConcurrentBag<IChangeEvent>()

        let newField = Array2D.copy field
        let width = Array2D.length1 field
        let height = Array2D.length2 field

        let processRow rowIndex rowWidth field =
            for columnIndex in 0..rowWidth - 1 do
                let current = Array2D.get field rowIndex columnIndex
                let isAlive = settings.Ruleset.IsAlive(field, rowIndex, columnIndex)
                if current <> isAlive then
                    newField.[rowIndex, columnIndex] <- isAlive
                    ChangeEvent(rowIndex, columnIndex, isAlive) |> events.Add

        seq { for rowIndex in 0..height - 1 do
                async { processRow rowIndex width field }
        }
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore

        field <- newField
        generation <- generation + 1L
        
        events.ToArray() |> render
    
    interface IGame with
        member this.Prepare() = prepare()
        member this.MakeNextGeneration() = makeNextGen()