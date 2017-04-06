namespace Readwell.Data

module GoogleBooksClient =

    open Google.Apis.Books.v1
    open Google.Apis.Services

    open Readwell.Entities
    open Readwell.Domain

    type GoogleBookDataClient = {
        search: string -> Book seq;
        associated: string -> Book seq;
    }

    type GoogleApiConfiguration = {
        applicationName: string;
        apiKey: string;
    }

    let getService configuration =
        let initializer = new BaseClientService.Initializer ( ApplicationName = configuration.applicationName, ApiKey = configuration.apiKey )
        new BooksService(initializer)

    let search (service:BooksService) searchTerms = 
        service.Volumes.List(searchTerms).Execute().Items |> Seq.map Book.ofVolume

    let associated (service:BooksService) id =
        service.Volumes.Associated.List(id).Execute().Items |> Seq.map Book.ofVolume

    let create config =
        let service = getService config
        {
            search = search service;
            associated = associated service;
        }
