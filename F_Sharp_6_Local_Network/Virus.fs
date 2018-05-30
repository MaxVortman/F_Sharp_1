namespace LocalNetwork

open System

type Virus(name : string, description : string, strength : LevelEnum) = 
    member val Name = name with get
    member val Description = description with get
    override this.ToString() = String.Format("This virus {0}\nDescription:\n{1}\nIts power: {2}%", 
                                                        this.Name, this.Description, this.TruePower)
    member val TruePower = strength with get