//
//  Album.swift
//  Musician2
//
//  Created by Maksim Ivanov on 05.07.2026.
//

import Foundation
import SwiftData

@Model
final class Album {
    @Attribute(.unique) var albumId: Int
    var albumName: String
    var albumYear: Int
    var albumCover: String
    var albumMedianColor: String
    @Relationship(deleteRule: .cascade) var tracks: [Track]

    init(albumId: Int, albumName: String, albumYear: Int, albumCover: String, albumMedianColor: String, tracks: [Track]) {
        self.albumId = albumId
        self.albumName = albumName
        self.albumYear = albumYear
        self.albumCover = albumCover
        self.albumMedianColor = albumMedianColor
        self.tracks = tracks
    }
}

extension Album: Codable {
    enum CodingKeys: String, CodingKey {
        case albumId, albumName, albumYear, albumCover, albumMedianColor, tracks
    }

    convenience init(from decoder: Decoder) throws {
        let container = try decoder.container(keyedBy: CodingKeys.self)
        let albumId = try container.decode(Int.self, forKey: .albumId)
        let albumName = try container.decode(String.self, forKey: .albumName)
        let albumYear = try container.decode(Int.self, forKey: .albumYear)
        let albumCover = try container.decode(String.self, forKey: .albumCover)
        let albumMedianColor = try container.decode(String.self, forKey: .albumMedianColor)
        let tracks = try container.decode([Track].self, forKey: .tracks)
        self.init(albumId: albumId, albumName: albumName, albumYear: albumYear, albumCover: albumCover, albumMedianColor: albumMedianColor, tracks: tracks)
        tracks.forEach { $0.album = self }
    }

    func encode(to encoder: Encoder) throws {
        var container = encoder.container(keyedBy: CodingKeys.self)
        try container.encode(albumId, forKey: .albumId)
        try container.encode(albumName, forKey: .albumName)
        try container.encode(albumYear, forKey: .albumYear)
        try container.encode(albumCover, forKey: .albumCover)
        try container.encode(albumMedianColor, forKey: .albumMedianColor)
        try container.encode(tracks, forKey: .tracks)
    }
}