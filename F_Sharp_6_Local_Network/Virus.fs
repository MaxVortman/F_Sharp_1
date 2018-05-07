namespace LocalNetwork

open System

type Virus(name : string, author : string, description : string, strength : LevelEnum) = 
    member val Name = name with get
    member val Description = description with get
    member val Author = author with get
    override this.ToString() = String.Format("This virus {0} had written by that genius -- {1}\nHere what it exactly doing, poor fellow:\n{2}\nIts power: {3}%", 
                                                                                                            this.Name, this.Author, this.Description, this.TruePower)
    member val TruePower = strength with get