namespace LocalNetwork.Test

open NUnit.Framework
open FsUnit

module OperatingSystemTest =
    
    [<Test>]
    let ``should install antivirus`` () = 
        let os = Start.osArray.[0]
        let antivirus = Start.antivirusArray.[0]
        os.InstallAntivirus antivirus
        os.Antivirus.Name |> should equal antivirus.Name

module LocalNetworkTest = 
    open LocalNetwork
    
    let localNetwork () = 
        let os = Start.osArray.[0]
        let computers = [new Computer(0, os); new Computer(1, os)]
        let matrix = 
            [
            [|0; 1|];
            [|0; 0|]
            ]
        new LocalNetwork(computers, matrix)
    
    let virus = 
        Start.virusArray.[1]

    [<Test>]
    let ``should move and infect`` () =
        let localNetwork = localNetwork ()
        localNetwork.Infect 0 virus
        localNetwork.MoveStep ()
        localNetwork.Computers.[0].IsInfected |> should be True
        localNetwork.Computers.[1].IsInfected |> should be True

    [<Test>]
    let ``should move and no infect`` () =
        let localNetwork = localNetwork ()
        localNetwork.Infect 1 virus
        localNetwork.MoveStep ()
        localNetwork.Computers.[0].IsInfected |> should be False
        localNetwork.Computers.[1].IsInfected |> should be True