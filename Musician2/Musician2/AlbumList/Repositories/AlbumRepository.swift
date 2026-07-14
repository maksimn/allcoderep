//
//  AlbumRepository.swift
//  Musician2
//
//  Created by Maksim Ivanov on 05.07.2026.
//

import Foundation
import SwiftData

final class AlbumRepository {
    private let dataLoader: NetworkDataLoader
    private let modelContext: ModelContext

    init(dataLoader: NetworkDataLoader, modelContext: ModelContext) {
        self.dataLoader = dataLoader
        self.modelContext = modelContext
    }

    func fetchAlbums() async throws -> [Album] {
        let url = URL(string: "http://maksimn.github.io/albums.json")!
        let data = try await dataLoader.download(url)
        let albums = try JSONDecoder().decode([Album].self, from: data)

        // Cache the fetched albums in SwiftData.
        for album in albums {
            modelContext.insert(album)
        }
        try modelContext.save()

        return albums
    }

    func loadCachedAlbums() -> [Album] {
        let descriptor = FetchDescriptor<Album>(sortBy: [SortDescriptor(\.albumId)])
        return (try? modelContext.fetch(descriptor)) ?? []
    }
}