﻿namespace LocalNetwork

open System

type Computer(id : int, operatingSystem : LocalNetwork.OperatingSystem) = 
    let defaultVirus = Virus("None", "Not working virus", LevelEnum.None)
    let mutable currentVirus = defaultVirus
    let mutable isInfected = false
    let setVirus virus =    currentVirus <- virus
                            isInfected <- true 
    member val Id = id with get
    member val OperatingSystem = operatingSystem with get
    member this.IsInfected = isInfected
    member this.Virus = currentVirus
    member this.Infect (virus : Virus) = 
        let infectionProbability =  int (this.OperatingSystem.HoleLevel + virus.TruePower - this.OperatingSystem.Antivirus.SecureLevel)        
        let rnd_value = let rnd = new System.Random()
                        rnd.Next(1, 100)
        if rnd_value <= infectionProbability then setVirus virus
    override this.ToString() = String.Format("Id {0}\nOperation System:\n{1}\nVirus:\n{2}%", this.Id, this.OperatingSystem, this.Virus)