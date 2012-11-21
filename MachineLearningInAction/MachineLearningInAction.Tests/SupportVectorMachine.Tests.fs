﻿namespace MachineLearning.Tests

open MachineLearning.SupportVectorMachine
open NUnit.Framework
open FsUnit

[<TestFixture>]
type ``SVM tests`` () =

    [<Test>]
    member this.``dot verification`` () =

        let v1 = [ 1.0; 2.0 ]
        let v2 = [ 3.0; 4.0 ]

        dot v1 v2 |> should equal 11.0

    [<Test>]
    member this.``clip verification`` () =

        let min, max = 1.0, 3.0

        clip (min, max) 0.0 |> should equal min
        clip (min, max) 10.0 |> should equal max
        clip (min, max) min |> should equal min
        clip (min, max) max |> should equal max
        clip (min, max) 2.0 |> should equal 2.0

    [<Test>]
    member this.``nextAround verification`` () =
        let size = 5
        nextAround size 0 |> should equal 1
        nextAround size 4 |> should equal 0

    [<Test>]
    member this.``rowError verification`` () =
        let row1 = { Data = [ 1.0; 2.0 ]; Label = 1.0;  Alpha = 0.5 }
        let row2 = { Data = [ 3.0; 4.0 ]; Label = -1.0; Alpha = 0.0 }
        let row3 = { Data = [ 5.0; 6.0 ]; Label = -1.0;  Alpha = 1.0 }
        let rows = [ row1; row2; row3 ]
        let b = 7.0
        // predicted should be 
        // b + 0.5 * 1.0 * row1.row3 + -1.0 * 1.0 * row3.row3
        // 7.0 + 0.5 * 17.0 - 1.0 * 61.0 = 66.5
        // 7.0 + 8.5 - 61.0 = -45.5
        rowError rows b row3 |> should equal -44.5

    [<Test>]
    member this.``findLowHigh verification`` () =
        