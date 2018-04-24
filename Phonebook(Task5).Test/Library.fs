namespace Phonebook_Task5_.Test

open NUnit.Framework
open FsUnit

module ``tests for ICommand`` = 
   open UIFactory

   [<OneTimeSetUp>]
   let ui = UIFactory.createUI

   [<Test>]
   let ``should don't lie down``() = ui.GetCommand(5).Action()