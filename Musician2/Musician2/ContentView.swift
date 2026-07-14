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
    }
}

#Preview {
    ContentView()
        .modelContainer(for: [Item.self, Album.self, Track.self], inMemory: true)
}
