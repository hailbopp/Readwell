namespace Readwell.Entities

open System

type LogLevel =
    | Info
    | Warning
    | Error
    override x.ToString() =
        match x with
        | Info -> "[INFO]"
        | Warning -> "[WARNING]"
        | Error -> "[ERROR]"

type Log = 
    { level: LogLevel; message: string; }
    override x.ToString() =
        sprintf "%s %s" (x.level.ToString()) (x.message)

type BookIdentifier =
    | ISBN10 of string
    | ISBN13 of string
    | Other of string

type Book = {
    source: string;
    sourceIdentifier: string;
    industryIdentifiers: BookIdentifier seq;
    title: string;
    authors: string seq;
    publisher: string option;
    published: DateTime option;

    image: string option;
}
