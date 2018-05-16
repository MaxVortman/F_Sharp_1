namespace LocalNetwork

type Computer(operatingSystem : OperatingSystem) = 
    let defaultVirus = Virus("None", "No one", "Not working virus", LevelEnum.None)
    let mutable virus = defaultVirus
    member this.Infect (?virus : Virus) = 
        let infectionProbability = (this.OperatingSystem.HoleLevel + defaultArg virus.TruePower - this.OperatingSystem.Antivirus.SecureLevel) % 100
        match infectionProbability  with
        | 100 ->    
                    true
        |
    member val OperatingSystem = operatingSystem with get
    member val IsInfected : bool = false with get
    member val Virus = 
    
