namespace LocalNetwork

type Computer(operatingSystem : OperatingSystem) = 
    let defaultVirus = Virus("None", "No one", "Not working virus", LevelEnum.None)
    let mutable currentVirus = defaultVirus
    let mutable isInfected = false
    let setVirus virus =    currentVirus <- virus
                            isInfected <- true 
    member val Id : int = -1 with get, set
    member val OperatingSystem = operatingSystem with get
    member val IsInfected : bool = false with get
    member val Virus = currentVirus with get
    member this.Infect (virus : Virus) = 
        let infectionProbability =  int (this.OperatingSystem.HoleLevel + virus.TruePower - this.OperatingSystem.Antivirus.SecureLevel) % 100        
        let rnd_value = let rnd = new System.Random()
                        rnd.Next(1, 100)
        if rnd_value <= infectionProbability then setVirus virus