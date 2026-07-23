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
            viewModel: AudioPlayerViewModel(
                track: Track(trackId: 1, name: "Анна", url: "http://maksimn.github.io/elizarov/notebook/anna.mp3", duration: "1:08", album: nil),
                dataLoader: URLSessionNetworkDataLoader(),
                audioPlayerService: AVAudioPlayerService()
            )
        )
    }
}

#Preview {
    ContentView()
        .modelContainer(for: [Item.self, Album.self, Track.self], inMemory: true)
}
