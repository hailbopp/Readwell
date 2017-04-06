module Readwell.Utilities

open System

let inline (|?) (a: 'a option) b = if a.IsSome then a.Value else b
let inline (|??) (a: 'a Nullable) b = if a.HasValue then a.Value else b
