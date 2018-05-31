namespace LocalNetwork

open System.Linq

type LocalNetwork(computers : Computer list, adjacencyMatrix : int[] list) =
    let mutable infectedComputers : Computer list = []
    member val Computers = computers with get
    member val AdjacencyMatrix = adjacencyMatrix with get
    member this.Infect id virus =   let computer : Computer = computers.Where(fun c -> c.Id = id).FirstOrDefault()
                                    computer.Infect virus
                                    infectedComputers <- computer :: infectedComputers
    member this.MoveStep = 
        let matrixTravesal id virus = 
            let n = this.AdjacencyMatrix.[id].Length
            let rec secondLayerTravesal i = 
                match i with
                | i when i = n ->                               ()
                | i when this.AdjacencyMatrix.[id].[i] = 1 ->   this.Infect i virus
                                                                secondLayerTravesal (i + 1)
                | _ ->                                          secondLayerTravesal (i + 1)
            secondLayerTravesal 0
            

        let rec infectedComputersTravesal (computers : Computer list) = 
            match computers with
            | h :: t -> matrixTravesal h.Id h.Virus
                        infectedComputersTravesal t
            | [] -> ()
        infectedComputersTravesal infectedComputers

    member this.PrintInfo =
        let rec computersTravesal computers = 
            match computers with
            | h :: t    ->     printfn "%O" h
                               computersTravesal t
            | []        ->     ()            
        computersTravesal computers