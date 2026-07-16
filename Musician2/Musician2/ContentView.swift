//
//  ContentView.swift
//  Musician2
//
//  Created by Maksim Ivanov on 30.06.2026.
//

import SwiftUI
import SwiftData

struct ContentView: View {

    var body: some View {
        AlbumListFeature()
        AudioPlayerView(
            track: Track(trackId: 1, name: "Матрица", url: "http://maksimn.github.io/elizarov/notebook/matritsa.mp3", duration: "3:19", album: nil),
            viewModel: AudioPlayerViewModel(dataLoader: URLSessionNetworkDataLoader())
        )
    }
}

#Preview {
    ContentView()
        .modelContainer(for: [Item.self, Album.self, Track.self], inMemory: true)
}
