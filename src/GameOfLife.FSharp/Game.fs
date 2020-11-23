namespace GameOfLife.FSharp.Game

open System
open GameOfLife.Abstractions

type public ChangeEvent(x: int, y: int, isAlive: bool) =
    interface IChangeEvent with
        member this.X = x
        member this.Y = y
        member this.IsAlive = isAlive

type public GameState(generation: Int64, events: IChangeEvent[]) =
    interface IGameState with
        member this.Generation = generation
        member this.Events = events