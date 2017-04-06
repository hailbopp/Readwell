namespace Readwell.Domain

open System
open Google.Apis.Books.v1.Data
open Readwell.Entities

module internal BookIdentifier =
    let ofIndustryIdentifier (i: Volume.VolumeInfoData.IndustryIdentifiersData) =
        match i.Type with
        | "ISBN_10" -> ISBN10 i.Identifier
        | "ISBN_13" -> ISBN13 i.Identifier
        | _ -> Other i.Identifier

module internal Book =
    let ofVolume (volume: Volume) =
        {
            source = "GoogleBooks";
            sourceIdentifier = volume.Id;
            industryIdentifiers = volume.VolumeInfo.IndustryIdentifiers |> Seq.map BookIdentifier.ofIndustryIdentifier;
            title = volume.VolumeInfo.Title;
            authors = volume.VolumeInfo.Authors;
            publisher =
                match String.IsNullOrWhiteSpace volume.VolumeInfo.Publisher with
                | false -> Some volume.VolumeInfo.Publisher
                | _ -> None
            published = 
                match DateTime.TryParse volume.VolumeInfo.PublishedDate with
                | true, dt -> Some dt
                | _ -> None
            image =
                match box volume.VolumeInfo.ImageLinks with
                | null -> None
                | _ -> if String.IsNullOrWhiteSpace volume.VolumeInfo.ImageLinks.Thumbnail then None else Some volume.VolumeInfo.ImageLinks.Thumbnail
        }