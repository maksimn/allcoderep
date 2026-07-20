//
//  AudioPlayerViewModel.swift
//  Musician2
//
//  Created by Maksim Ivanov on 20.07.2026.
//

import Foundation
import Observation

enum PlayerState: Equatable {
    case initial, loading, loaded, playing, paused, error
}

@Observable
final class AudioPlayerViewModel {

    let track: Track

    private(set) var state: PlayerState = .initial

    private var data: Data?

    private let dataLoader: NetworkDataLoader

    private let audioPlayerService: AudioPlayerService

    init(track: Track, dataLoader: NetworkDataLoader, audioPlayerService: AudioPlayerService) {
        self.track = track
        self.dataLoader = dataLoader
        self.audioPlayerService = audioPlayerService
    }

    @MainActor
    func loadTrack() async {
        guard let url = URL(string: track.url) else { return }
        
        state = .loading

        do {
            data = try await dataLoader.download(url)
            state = .loaded
        } catch {
            state = .error
        }
    }

    @MainActor
    func play() {
        guard let data else { return }

        do {
            if state == .loaded {
                try audioPlayerService.initialize(with: data)
            }
            
            if state == .loaded || state == .paused {
                audioPlayerService.play()
                state = .playing
            } else if state == .playing {
                audioPlayerService.pause()
                state = .paused
            }
            
        } catch {
            state = .error
        }
    }
}
