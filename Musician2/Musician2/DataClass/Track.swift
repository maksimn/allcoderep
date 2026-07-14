//
//  Track.swift
//  Musician2
//
//  Created by Maksim Ivanov on 05.07.2026.
//

import Foundation
import SwiftData

@Model
final class Track {
    @Attribute(.unique) var trackId: Int
    var name: String
    var url: String
    var duration: String
    var album: Album?

    init(trackId: Int, name: String, url: String, duration: String, album: Album? = nil) {
        self.trackId = trackId
        self.name = name
        self.url = url
        self.duration = duration
        self.album = album
    }
}

extension Track: Codable {
    enum CodingKeys: String, CodingKey {
        case trackId, name, url, duration
    }

    convenience init(from decoder: Decoder) throws {
        let container = try decoder.container(keyedBy: CodingKeys.self)
        let trackId = try container.decode(Int.self, forKey: .trackId)
        let name = try container.decode(String.self, forKey: .name)
        let url = try container.decode(String.self, forKey: .url)
        let duration = try container.decode(String.self, forKey: .duration)
        self.init(trackId: trackId, name: name, url: url, duration: duration)
    }

    func encode(to encoder: Encoder) throws {
        var container = encoder.container(keyedBy: CodingKeys.self)
        try container.encode(trackId, forKey: .trackId)
        try container.encode(name, forKey: .name)
        try container.encode(url, forKey: .url)
        try container.encode(duration, forKey: .duration)
    }
}