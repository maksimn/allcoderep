//
//  AudioPlayerViewModel.swift
//  Musician2
//
//  Created by Maksim Ivanov on 20.07.2026.
//

import Foundation
import Observation

@Observable
final class AudioPlayerViewModel {

    private(set) var state: PlayerState = .initial

    private let dataLoader: NetworkDataLoader

    init(dataLoader: NetworkDataLoader) {
        self.dataLoader = dataLoader
    }

    @MainActor
    func loadTrack(from url: URL) async {
        state = .loading

        do {
            let _ = try await dataLoader.download(url)
            state = .loaded
        } catch {
            state = .initial
        }
    }
}
