namespace LocalNetwork

open System.Linq
/// <summary>
/// Локальная сеть
/// </summary>
type LocalNetwork(computers : Computer list, adjacencyMatrix : bool[] list) =
    let mutable infectedComputers : Computer list = []
    /// <summary>
    /// компьютеры в локальной сети
    /// </summary>
    member this.Computers = computers
    member val AdjacencyMatrix = adjacencyMatrix with get
    /// <summary>
    /// Инфицирование компьютера в сети по его id 
    /// </summary>
    /// <param name="id">Идентификатор компьютера</param>
    /// <param name="virus">Вирус, которым инфицируем</param>
    member this.Infect id virus =   let computer : Computer = computers |> List.filter (fun c -> c.Id = id) |> List.head
                                    computer.Infect virus
                                    infectedComputers <- computer :: infectedComputers
    /// <summary>
    /// Переход на следующую итерацию
    /// </summary>
    member this.MoveStep () = 
        let matrixTravesal id virus = 
            let n = this.AdjacencyMatrix.[id].Length
            let rec secondLayerTravesal i = 
                match i with
                | i when i = n ->                                   ()
                | i when this.AdjacencyMatrix.[id].[i] = true ->    this.Infect i virus
                                                                    secondLayerTravesal (i + 1)
                | _ ->                                              secondLayerTravesal (i + 1)
            secondLayerTravesal 0
            

        let rec infectedComputersTravesal (computers : Computer list) = 
            match computers with
            | h :: t -> matrixTravesal h.Id h.Virus
                        infectedComputersTravesal t
            | [] -> ()
        infectedComputersTravesal infectedComputers
    /// <summary>
    /// Печать на консоль состояние компьютеров в сети
    /// </summary>
    member this.PrintInfo =
        let rec computersTravesal computers = 
            match computers with
            | h :: t    ->     printfn "%O" h
                               computersTravesal t
            | []        ->     ()            
        computersTravesal computers