namespace LocalNetwork

open System
/// <summary>
/// Компьютер локальной сети
/// </summary>
type Computer(id : int, operatingSystem : LocalNetwork.OperatingSystem) = 
    let defaultVirus = Virus("None", "Not working virus", LevelEnum.None)
    let mutable currentVirus = defaultVirus
    let mutable isInfected = false
    let setVirus virus =    currentVirus <- virus
                            isInfected <- true 
    member val Id = id with get
    member val OperatingSystem = operatingSystem with get
    /// <summary>
    /// Получение состояния: инфицирован ли компьютер вирусом 
    /// </summary>
    member this.IsInfected = isInfected
    /// <summary>
    /// Получение option вируса
    /// </summary>
    member this.Virus = currentVirus
    /// <summary>
    /// Инфицирование вирусом системы
    /// </summary>
    /// <param name="virus">вирус, которым инфицируем</param>
    member this.Infect (virus : Virus) = 
        let infectionProbability =  int (this.OperatingSystem.HoleLevel + virus.TruePower - this.OperatingSystem.Antivirus.SecureLevel)        
        let rndValue =  let rnd = new System.Random()
                        rnd.Next(1, 100)
        if rndValue <= infectionProbability then setVirus virus
    /// <summary>
    /// Переопределение ToString object-а
    /// </summary>
    override this.ToString() = String.Format("Id {0}\nOperation System:\n{1}\nVirus:\n{2}%", this.Id, this.OperatingSystem, this.Virus)