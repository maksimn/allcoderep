//
//  AudioPlayerViewModel.swift
//  Musician2
//
//  Created by Maksim Ivanov on 20.07.2026.
//

import Foundation
import Observation

enum AudioPlayerState: Equatable {
    case initial, loading, loaded, playing, paused, error
}

@Observable
final class AudioPlayerViewModel {

    let track: Track

    private(set) var state: AudioPlayerState = .initial

    private(set) var progress = 0.0

    private(set) var currentTime: TimeInterval = 0.0

    private var data: Data?

    private var timer: Timer?

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
                stopProgressTimer()
                startProgressTimer()
            } else if state == .playing {
                audioPlayerService.pause()
                state = .paused
                stopProgressTimer()
            }
        } catch {
            state = .error
        }
    }

    // MARK: - Progress tracking

    private func startProgressTimer() {
        timer = Timer.scheduledTimer(withTimeInterval: 0.25, repeats: true) { [weak self] _ in
            Task { @MainActor [weak self] in
                guard let self else { return }

                let duration = audioPlayerService.duration

                guard duration > 0 else { return }

                currentTime = audioPlayerService.currentTime
                progress = currentTime / duration
            }
        }
    }

    private func stopProgressTimer() {
        timer?.invalidate()
        timer = nil
    }

    deinit {
        timer?.invalidate()
    }
}
