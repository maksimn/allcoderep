//
//  AlbumListFeature.swift
//  Musician2
//
//  Created by Maksim Ivanov on 16.07.2026.
//

import SwiftUI
import SwiftData

struct AlbumListFeature: View {

    @Environment(\.modelContext) private var modelContext

    var body: some View {
        AlbumListView(
            viewModel: AlbumListViewModel(
                repository: AlbumRepository(
                    dataLoader: URLSessionNetworkDataLoader(),
                    modelContext: modelContext
                )
            )
        )
    }
}
